using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Data.Abstract;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Handlers.CommandHandlers
{
    public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, Result>
    {
        private readonly IUserDal _userDal;
        public PasswordResetCommandHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<Result> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _userDal.PasswordReset(request.Email, request.Token, request.Password);
            if (!response.IsSuccess)
                return Result.Fail(response.Errors);

            return Result.Ok();
        }
    }
}
