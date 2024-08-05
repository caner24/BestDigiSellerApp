
using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Application.User.Commands.Response;
using BestDigiSellerApp.User.Data.Abstract;
using BestDigiSellerApp.User.Entity.Dto;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Handlers.CommandHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, Result<RegisterUserCommandResponse>>
    {
        private readonly IUserDal _userDal;
        public RegisterUserCommandHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<Result<RegisterUserCommandResponse>> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            var registerUser = await _userDal.RegisterUser(request);
            if (!registerUser.Succeeded)
            {
                var result = new Result();
                foreach (var error in registerUser.Errors)
                {
                    result.WithError(new Error(error.Description).WithMetadata("Code", error.Code));
                }
                return result;
            }
            var confirmationToken = await _userDal.GenerateEmailConfirmationToken(request.Email);

            return Result.Ok(new RegisterUserCommandResponse { ConfirmationToken = confirmationToken.Value, Email = request.Email });
        }
    }
}
