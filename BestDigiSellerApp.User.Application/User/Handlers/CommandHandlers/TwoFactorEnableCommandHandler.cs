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
    public class TwoFactorEnableCommandHandler : IRequestHandler<TwoFactorEnableRequest, Result>
    {
        private readonly IUserDal _userDal;
        public TwoFactorEnableCommandHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<Result> Handle(TwoFactorEnableRequest request, CancellationToken cancellationToken)
        {
            var response = await _userDal.Manage2Factor();
            if (!response.IsSuccess)
                return Result.Fail(response.Errors);

            return Result.Ok();
        }
    }
}
