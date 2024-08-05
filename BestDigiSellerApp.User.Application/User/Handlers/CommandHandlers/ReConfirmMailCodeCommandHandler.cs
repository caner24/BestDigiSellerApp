
using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Application.User.Commands.Response;
using BestDigiSellerApp.User.Data.Abstract;
using BestDigiSellerApp.User.Entity;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Handlers.CommandHandlers
{
    public class ReConfirmMailCodeCommandHandler : IRequestHandler<ReConfirmMailCodeCommandRequest, Result<ReConfirmMailCodeCommandResponse>>
    {
        private readonly IUserDal _userDal;
        public ReConfirmMailCodeCommandHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<Result<ReConfirmMailCodeCommandResponse>> Handle(ReConfirmMailCodeCommandRequest request, CancellationToken cancellationToken)
        {
            var userToken = await _userDal.GenerateEmailConfirmationToken(request.EmailAdress);
            if (userToken.IsFailed)
                return Result.Fail(userToken.Errors);

            return Result.Ok(new ReConfirmMailCodeCommandResponse { Token = userToken.Value, Email = request.EmailAdress });
        }
    }
}
