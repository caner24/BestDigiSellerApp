
using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Application.User.Commands.Response;
using BestDigiSellerApp.User.Data.Abstract;
using BestDigiSellerApp.User.Entity.Dto;
using BestDigiSellerApp.User.Entity.Exceptions;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Handlers.CommandHandlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUserDal _userDal;
        public LoginUserCommandHandler(IPublishEndpoint publishEndpoint, IUserDal userDal)
        {
            _publishEndpoint = publishEndpoint;
            _userDal = userDal;
        }
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userDal.LogInUser(request);
            if (!user.Succeeded)
            {
                if (user.IsLockedOut)
                {
                    throw new UserLockedOutException();
                }
                else if (user.IsNotAllowed)
                {
                    throw new UserNotAllowedException();
                }
                else if (user.RequiresTwoFactor)
                {
                    var twoStepTokenCode = await _userDal.GenerateTwoStep(request.UserEmail);
                    await _publishEndpoint.Publish<TwoStepLoginDto>(new TwoStepLoginDto { Email = request.UserEmail, Token = twoStepTokenCode });
                    return new LoginUserCommandResponse { IsTwoStepVerification = true };
                }
                else
                {
                    throw new UserNotAllowedException();
                }
            }
            var bearerDetails = await _userDal.CreateToken(true);
            return new LoginUserCommandResponse
            {
                AccessToken = bearerDetails.AccessToken,
                ExpireTime = bearerDetails.ExpireTime,
                RefreshToken = bearerDetails.RefreshToken
            };
        }
    }
}
