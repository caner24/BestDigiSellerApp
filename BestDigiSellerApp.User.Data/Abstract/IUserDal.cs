using BestDigiSellerApp.User.Entity.Dto;
using FluentResults;
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
        Task<Result<SignInResult>> LogInUser(UserForLoginDto userForAuthDto);
        Task<Result<SignInResult>> LoginTwoStep(LoginTwoStepDto loginTwoStepDto);
        Task<Result<string>> GenerateTwoStep(string email);
        Task<Result<TokenDto>> CreateToken(bool populateExp);
        Task<Result<TokenDto>> RefreshToken(TokenDto tokenDto);
        Task<Result<string>> GenerateEmailConfirmationToken(string email);
        Task<Result<string>> GeneratePasswordResetToken(string email);
        Task<Result<string>> GenerateChangePhoneNumber(string email);
        Task<Result<IdentityResult>> ConfirmEmailToken(string email, string token);

    }
}
