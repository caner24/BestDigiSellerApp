
using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Application.User.Commands.Response;
using BestDigiSellerApp.User.Data.Abstract;
using BestDigiSellerApp.User.Entity.Exceptions;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Handlers.CommandHandlers
{
    public class LoginTwoStepCommandHandler : IRequestHandler<LoginTwoStepCommandRequest, Result<LoginTwoStepCommandResponse>>
    {
        private readonly IUserDal _userDal;
        public LoginTwoStepCommandHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<Result<LoginTwoStepCommandResponse>> Handle(LoginTwoStepCommandRequest request, CancellationToken cancellationToken)
        {
            var loginResult = await _userDal.LoginTwoStep(request);
            if (loginResult.IsFailed)
                return Result.Fail(loginResult.Errors);

            if (!loginResult.Value.Succeeded)
            {
                if (loginResult.Value.IsLockedOut)
                {
                    return new UserLockedOutResult();
                }
                else
                {
                    return new WrongTwoStepCodeResult();
                }
            }
            var bearerDetails = await _userDal.CreateToken(true);
            return Result.Ok(new LoginTwoStepCommandResponse
            {
                AccessToken = bearerDetails.Value.AccessToken,
                ExpireTime = bearerDetails.Value.ExpireTime,
                RefreshToken = bearerDetails.Value.RefreshToken
            });
        }
    }
}
