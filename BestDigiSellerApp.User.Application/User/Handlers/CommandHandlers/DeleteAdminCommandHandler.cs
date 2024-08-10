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
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommandRequest, Result>
    {
        private readonly IUserDal _userDal;
        public DeleteAdminCommandHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<Result> Handle(DeleteAdminCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _userDal.DeleteAdminUser(request.Email);
            if (!result.IsSuccess)
                return Result.Fail(result.Errors);

            return Result.Ok();
        }
    }
}
