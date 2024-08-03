using AutoMapper;
using BestDigiSellerApp.User.Data.Abstract;
using BestDigiSellerApp.User.Entity.Dto;
using BestDigiSellerApp.User.Entity.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User.Entity.User> _userManager;
        private readonly SignInManager<User.Entity.User> _signInManager;
        private readonly IConfiguration _configuration;

        public UserDal(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            UserManager<User.Entity.User> userManager,
            SignInManager<User.Entity.User> signInManager,
        IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                throw new SessionException();

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
            return new TokenDto()
            {
                ExpireTime = user.RefreshTokenExpiryTime,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            var user = _mapper.Map<User.Entity.User>(userForRegistrationDto);

            var result = await _userManager
                .CreateAsync(user, userForRegistrationDto.Password);
            return result;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto)
        {
            var user = await _userManager.FindByEmailAsync(userForAuthDto.UserEmail);
            var result = (user != null && await _userManager.CheckPasswordAsync(user, userForAuthDto.Password));
            if (!result)
            {
                Log.Warning($"{nameof(ValidateUser)} : Authentication failed. Wrong username or password.");
            }
            return result;
        }
        public async Task<SignInResult> LogInUser(UserForLoginDto userForAuthDto)
        {
            var user = await _userManager.FindByEmailAsync(userForAuthDto.UserEmail);
            if (user == null)
                return SignInResult.Failed;

            var result = await _signInManager.PasswordSignInAsync(user, userForAuthDto.Password, userForAuthDto.IsRemember, true);
            return result;
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

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user is null ||
                user.RefreshToken != tokenDto.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new RefreshTokenExpiredException();

            return await CreateToken(populateExp: false);
        }

        public async Task<string> GenerateEmailConfirmationToken(string email)
        {
            var isUserExist = await _userManager.FindByEmailAsync(email);

            if (isUserExist == null)
                throw new UserNotFoundException();

            if (isUserExist.EmailConfirmed)
                throw new EmailConfirmedException();


            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(isUserExist);

            return HttpUtility.UrlEncode(confirmationToken)
;
        }

        public async Task<string> GeneratePasswordResetToken(string email)
        {
            var isUserExist = await _userManager.FindByEmailAsync(email);
            if (isUserExist is null)
                throw new Exception("User was not founded!.");

            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(isUserExist);

            return HttpUtility.UrlEncode(passwordResetToken);
        }

        public async Task<string> GenerateChangePhoneNumber(string email)
        {
            var isUserExist = await _userManager.FindByEmailAsync(email);
            if (isUserExist is null)
                throw new UserNotFoundException();

            var passwordResetToken = await _userManager.GenerateChangePhoneNumberTokenAsync(isUserExist, isUserExist.PhoneNumber);

            return HttpUtility.UrlEncode(passwordResetToken);
        }

        public async Task<IdentityResult> ConfirmEmailToken(string email, string token)
        {

            var isUserExist = await _userManager.FindByEmailAsync(email);
            if (isUserExist is null)
                throw new UserNotFoundException();

            var isTokenValid = await _userManager.VerifyUserTokenAsync(isUserExist, "EmailConfirmation", "ConfirmEmail", token);
            if (isTokenValid)
                throw new EmailTokenExpireException();

            var identityResult = await _userManager.ConfirmEmailAsync(isUserExist, HttpUtility.UrlDecode(token));
            return identityResult;
        }

        public async Task<string> GenerateTwoStep(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var step = await _userManager.GetTwoFactorEnabledAsync(user);
            if (step is false)
                await _userManager.SetTwoFactorEnabledAsync(user, true);

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
            return token;
        }

        public async Task<SignInResult> LoginTwoStep(LoginTwoStepDto loginTwoStepDto)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
                throw new UserNotAllowedException();

            var result = await _signInManager.TwoFactorSignInAsync("Email", loginTwoStepDto.Code, loginTwoStepDto.IsPersident, loginTwoStepDto.RememberClient);
            return result;
        }

    }

}
