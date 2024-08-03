using BestDigiSellerApp.User.Entity.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Data.Abstract
{
    public interface IUserDal
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
        Task<SignInResult> LogInUser(UserForLoginDto userForAuthDto);
        Task<SignInResult> LoginTwoStep(LoginTwoStepDto loginTwoStepDto);
        Task<string> GenerateTwoStep(string email);
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
        Task<string> GenerateEmailConfirmationToken(string email);
        Task<string> GeneratePasswordResetToken(string email);
        Task<string> GenerateChangePhoneNumber(string email);
        Task<IdentityResult> ConfirmEmailToken(string email, string token);

    }
}
