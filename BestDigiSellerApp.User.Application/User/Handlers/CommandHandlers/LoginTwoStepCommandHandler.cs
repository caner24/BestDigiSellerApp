
using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Application.User.Commands.Response;
using BestDigiSellerApp.User.Data.Abstract;
using BestDigiSellerApp.User.Entity.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Handlers.CommandHandlers
{
    public class LoginTwoStepCommandHandler : IRequestHandler<LoginTwoStepCommandRequest, LoginTwoStepCommandResponse>
    {
        private readonly IUserDal _userDal;
        public LoginTwoStepCommandHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<LoginTwoStepCommandResponse> Handle(LoginTwoStepCommandRequest request, CancellationToken cancellationToken)
        {
            var loginResult = await _userDal.LoginTwoStep(request);
            if (!loginResult.Succeeded)
            {
                if (loginResult.IsLockedOut)
                {
                    throw new UserLockedOutException();
                }
                else
                {
                    return new LoginTwoStepCommandResponse { IsWrong2Step = true };
                }
            }
            var bearerDetails = await _userDal.CreateToken(true);
            return new LoginTwoStepCommandResponse
            {
                AccessToken = bearerDetails.AccessToken,
                ExpireTime = bearerDetails.ExpireTime,
                RefreshToken = bearerDetails.RefreshToken
            };
        }
    }
}
