using BestDigiSellerApp.User.Application.User.Queries.Request;
using BestDigiSellerApp.User.Application.User.Queries.Response;
using BestDigiSellerApp.User.Data.Abstract;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Handlers.QueryHandlers
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQueryRequest, Result<GetAllUserEmailResponse>>
    {
        private readonly IUserDal _userDal;
        public GetAllUserQueryHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<Result<GetAllUserEmailResponse>> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userDal.GetUsersEmail();
            if (!users.IsSuccess)
                return Result.Fail(users.Errors);

            return new GetAllUserEmailResponse { Email = users.Value };
        }
    }
}
