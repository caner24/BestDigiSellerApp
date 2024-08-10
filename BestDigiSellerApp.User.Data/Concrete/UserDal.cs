using AutoMapper;
using BestDigiSellerApp.User.Data.Abstract;
using BestDigiSellerApp.User.Entity.Dto;
using BestDigiSellerApp.User.Entity.Exceptions;
using BestDigiSellerApp.User.Entity.Results;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BestDigiSellerApp.User.Data.Concrete
{
    public class UserDal : IUserDal
    {
        string? confirmEmailEndpointName = null;
        private readonly IMapper _mapper;
        private readonly UserContext _userContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User.Entity.User> _userManager;
        private readonly SignInManager<User.Entity.User> _signInManager;
        private readonly IConfiguration _configuration;

        public UserDal(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            UserContext userContext,
            UserManager<User.Entity.User> userManager,
            SignInManager<User.Entity.User> signInManager,
        IConfiguration configuration)
        {
            _userContext = userContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<Result<TokenDto>> CreateToken(bool populateExp)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return new SessionResult();

            var signinCredentials = GetSignInCredentials();
            var claims = _httpContextAccessor.HttpContext.User.Claims.Where(c =>
                c.Type == ClaimTypes.Name || c.Type == ClaimTypes.Email || c.Type == ClaimTypes.Role);
            var tokenOptions = GenerateTokenOptions(signinCredentials, claims);
            var refreshToken = GenerateRefreshToken();

            var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            user.RefreshToken = refreshToken;

            if (populateExp)
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Result.Ok(new TokenDto()
            {
                ExpireTime = user.RefreshTokenExpiryTime,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            var user = _mapper.Map<User.Entity.User>(userForRegistrationDto);

            var result = await _userManager
                .CreateAsync(user, userForRegistrationDto.Password);
            return result;
        }

        public async Task<Result<SignInResult>> LogInUser(UserForLoginDto userForAuthDto)
        {
            var user = await _userManager.FindByEmailAsync(userForAuthDto.UserEmail);
            if (user == null)
                return new UserNotFoundResult();

            var result = await _signInManager.PasswordSignInAsync(user, userForAuthDto.Password, userForAuthDto.IsRemember, true);
            return Result.Ok(result);
        }

        private SigningCredentials GetSignInCredentials()
        {
            var validIssuer = _configuration["ValidIssuer"];
            var key = Encoding.UTF8.GetBytes(_configuration["SecretKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials,
            IEnumerable<Claim> claims)
        {
            var validIssuer = _configuration["ValidIssuer"];
            var expires = _configuration["Expires"];

            var tokenOptions = new JwtSecurityToken(
                    issuer: validIssuer,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(expires)),
                    signingCredentials: signinCredentials);

            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var secretKey = _configuration["SecretKey"];
            var validIssuer = _configuration["ValidIssuer"];

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = validIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
                out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken is null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }
            return principal;
        }

        public async Task<Result<TokenDto>> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user is null ||
                user.RefreshToken != tokenDto.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
                return new RefreshTokenExpiredResult();

            return await CreateToken(populateExp: false);
        }

        public async Task<Result<string>> GenerateEmailConfirmationToken(string email)
        {
            var isUserExist = await _userManager.FindByEmailAsync(email);

            if (isUserExist == null)
                return new UserNotFoundResult();

            if (isUserExist.EmailConfirmed)
                return new EmailConfirmedResult();


            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(isUserExist);

            return Result.Ok(HttpUtility.UrlEncode(confirmationToken));
            ;
        }

        public async Task<Result<string>> GeneratePasswordResetToken(string email)
        {
            var isUserExist = await _userManager.FindByEmailAsync(email);
            if (isUserExist is null)
                return new UserNotFoundResult();

            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(isUserExist);

            return Result.Ok(HttpUtility.UrlEncode(passwordResetToken));
        }

        public async Task<Result<IdentityResult>> ConfirmEmailToken(string email, string token)
        {

            var isUserExist = await _userManager.FindByEmailAsync(email);
            if (isUserExist is null)
                return new UserNotFoundResult();

            var isTokenValid = await _userManager.VerifyUserTokenAsync(isUserExist, TokenOptions.DefaultProvider, "ConfirmEmail", token);
            if (isTokenValid)
                return new EmailTokenExpireResult();

            var identityResult = await _userManager.ConfirmEmailAsync(isUserExist, HttpUtility.UrlDecode(token));
            return Result.Ok(identityResult);
        }

        public async Task<Result<string>> GenerateTwoStep(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var step = await _userManager.GetTwoFactorEnabledAsync(user);
            if (step is false)
                return new TwoStepIsInactiveResult();

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
            return Result.Ok(token);
        }

        public async Task<Result<SignInResult>> LoginTwoStep(LoginTwoStepDto loginTwoStepDto)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
                return new UserNotAllowedResult();

            var result = await _signInManager.TwoFactorSignInAsync("Email", loginTwoStepDto.Code, loginTwoStepDto.IsPersident, loginTwoStepDto.RememberClient);
            return Result.Ok(result);
        }

        public async Task<Result> CreateAdminUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return Result.Fail(new UserNotFoundResult());
            if (await _userManager.IsInRoleAsync(user, "Admin"))
                return Result.Fail(new UserAlreadyAdminResult());

            await _userManager.AddToRoleAsync(user, "Admin");

            return Result.Ok();
        }

        public async Task<Result> DeleteAdminUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return Result.Fail(new UserNotFoundResult());

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            }


            return Result.Ok();
        }

        public async Task<Result<IdentityResult>> PasswordReset(string email, string token, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return Result.Fail(new UserNotFoundResult());

            var resetPassword = await _userManager.ResetPasswordAsync(user, token, password);
            if (!resetPassword.Succeeded)
                return Result.Fail<IdentityResult>(resetPassword.Errors.Select(x => x.Description));

            return Result.Ok(resetPassword);
        }

        public async Task<Result<List<string>>> GetUsersEmail()
        {
            var users = await _userContext.Users.Select(x => x.Email).ToListAsync();
            if (users is null)
                return Result.Fail(new UserNotFoundResult());

            return Result.Ok(users);
        }

        public async Task<Result> Manage2Factor()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
            if (userEmail is null)
                return Result.Fail(new UserNotFoundResult());

            var user = await _userManager.FindByEmailAsync(userEmail);
            if (!user.TwoFactorEnabled)
            {
                user.TwoFactorEnabled = true;
            }
            return Result.Ok();

        }
    }

}
