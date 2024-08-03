
using BestDigiSellerApp.User.Application.User.Queries.Request;
using BestDigiSellerApp.User.Application.User.Queries.Response;
using BestDigiSellerApp.User.Data.Abstract;
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
    public class ConfirmMailQueryHandler : IRequestHandler<ConfirmMailQueryRequest, ConfirmMailQueryResponse>
    {
        private readonly IUserDal _userDal;
        public ConfirmMailQueryHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<ConfirmMailQueryResponse> Handle(ConfirmMailQueryRequest request, CancellationToken cancellationToken)
        {
            var confirmMail = await _userDal.ConfirmEmailToken(request.Email, request.Token);
            if (!confirmMail.Succeeded)
            {
                ValidationException ex = new ValidationException();
                foreach (var item in confirmMail.Errors)
                {
                    ex.Data.Add(item.Code, item.Description);
                }
                throw ex;
            }
            return new ConfirmMailQueryResponse { IsOk = true };
        }
    }
}
