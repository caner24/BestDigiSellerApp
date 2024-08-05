
using BestDigiSellerApp.User.Application.User.Queries.Request;
using BestDigiSellerApp.User.Data.Abstract;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Handlers.QueryHandlers
{
    public class ConfirmMailQueryHandler : IRequestHandler<ConfirmMailQueryRequest, Result>
    {
        private readonly IUserDal _userDal;
        public ConfirmMailQueryHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<Result> Handle(ConfirmMailQueryRequest request, CancellationToken cancellationToken)
        {
            var confirmMail = await _userDal.ConfirmEmailToken(request.Email, request.Token);
            if (!confirmMail.Value.Succeeded)
            {
                var result = new Result();
                foreach (var error in confirmMail.Value.Errors)
                {
                    result.WithError(new Error(error.Description).WithMetadata("Code", error.Code));
                }
                return result;
            }
            return Result.Ok();
        }
    }
}
