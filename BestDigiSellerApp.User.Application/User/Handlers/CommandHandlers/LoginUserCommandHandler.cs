
using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Application.User.Commands.Response;
using BestDigiSellerApp.User.Data.Abstract;
using BestDigiSellerApp.User.Entity.Dto;
using BestDigiSellerApp.User.Entity.Exceptions;
using FluentResults;
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
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, Result<LoginUserCommandResponse>>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUserDal _userDal;
        public LoginUserCommandHandler(IPublishEndpoint publishEndpoint, IUserDal userDal)
        {
            _publishEndpoint = publishEndpoint;
            _userDal = userDal;
        }
        public async Task<Result<LoginUserCommandResponse>> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userDal.LogInUser(request);

            if (user.IsFailed)
                return Result.Fail(user.Errors);

            if (!user.Value.Succeeded)
            {
                if (user.Value.IsLockedOut)
                {
                    return new UserLockedOutResult();
                }
                else if (user.Value.IsNotAllowed)
                {
                    return new UserNotAllowedResult();
                }
                else if (user.Value.RequiresTwoFactor)
                {
                    var twoStepTokenCode = await _userDal.GenerateTwoStep(request.UserEmail);
                    await _publishEndpoint.Publish<TwoStepLoginDto>(new TwoStepLoginDto { Email = request.UserEmail, Token = twoStepTokenCode.Value });
                    return new TwoStepRequiredResult();
                }
                else
                {
                    return new UserNotAllowedResult();
                }
            }
            var bearerDetails = await _userDal.CreateToken(true);
            return Result.Ok(new LoginUserCommandResponse
            {
                AccessToken = bearerDetails.Value.AccessToken,
                ExpireTime = bearerDetails.Value.ExpireTime,
                RefreshToken = bearerDetails.Value.RefreshToken
            });
        }
    }
}
