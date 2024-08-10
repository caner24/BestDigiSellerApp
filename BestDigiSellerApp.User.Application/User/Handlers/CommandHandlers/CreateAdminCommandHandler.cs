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
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommandRequest, Result>
    {
        private readonly IUserDal _userDal;
        public CreateAdminCommandHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<Result> Handle(CreateAdminCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _userDal.CreateAdminUser(request.Email);
            if (!result.IsSuccess)
                return Result.Fail(result.Errors);

            return Result.Ok();
        }
    }
}
