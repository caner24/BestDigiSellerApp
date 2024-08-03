
using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Application.User.Commands.Response;
using BestDigiSellerApp.User.Data.Abstract;
using BestDigiSellerApp.User.Entity.Dto;
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
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        private readonly IUserDal _userDal;
        public RegisterUserCommandHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            var registerUser = await _userDal.RegisterUser(request);
            if (!registerUser.Succeeded)
            {
                ValidationException ex = new ValidationException();
                foreach (var item in registerUser.Errors)
                {
                    ex.Data.Add(item.Code, item.Description);
                }
                throw ex;
            }
            var confirmationToken = await _userDal.GenerateEmailConfirmationToken(request.Email);

            return new RegisterUserCommandResponse { ConfirmationToken = confirmationToken, Email = request.Email };
        }
    }
}
