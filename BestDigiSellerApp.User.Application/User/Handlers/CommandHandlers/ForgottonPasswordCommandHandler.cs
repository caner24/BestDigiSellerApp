using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Application.User.Commands.Response;
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
    public class ForgottonPasswordCommandHandler : IRequestHandler<ForgottenPasswordCommandRequest, Result<ForgottonPasswordCommandRepsonse>>
    {
        private readonly IUserDal _userDal;
        public ForgottonPasswordCommandHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<Result<ForgottonPasswordCommandRepsonse>> Handle(ForgottenPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _userDal.GeneratePasswordResetToken(request.Email);
            if (!response.IsSuccess)
                return Result.Fail(response.Errors);

            return Result.Ok(new ForgottonPasswordCommandRepsonse { Token = response.Value });
        }
    }
}
