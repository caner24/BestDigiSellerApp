
using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Application.User.Commands.Response;
using BestDigiSellerApp.User.Data.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Handlers.CommandHandlers
{
    public class ReConfirmMailCodeCommandHandler : IRequestHandler<ReConfirmMailCodeCommandRequest, ReConfirmMailCodeCommandResponse>
    {
        private readonly IUserDal _userDal;
        public ReConfirmMailCodeCommandHandler(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<ReConfirmMailCodeCommandResponse> Handle(ReConfirmMailCodeCommandRequest request, CancellationToken cancellationToken)
        {
            var userToken = await _userDal.GenerateEmailConfirmationToken(request.EmailAdress);
            return new ReConfirmMailCodeCommandResponse { Token = userToken, Email = request.EmailAdress };
        }
    }
}
