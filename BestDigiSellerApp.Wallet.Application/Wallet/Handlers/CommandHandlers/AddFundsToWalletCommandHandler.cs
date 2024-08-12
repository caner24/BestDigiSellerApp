using BestDigiSellerApp.Wallet.Application.Wallet.Commands.Request;
using BestDigiSellerApp.Wallet.Application.Wallet.Commands.Response;
using BestDigiSellerApp.Wallet.Data.Abstract;
using BestDigiSellerApp.Wallet.Entity.Enums;
using BestDigiSellerApp.Wallet.Entity.Results;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Application.Wallet.Handlers.CommandHandlers
{
    public class AddFundsToWalletCommandHandler : IRequestHandler<AddFundsToWalletCommandRequest, Result>
    {
        private readonly IWalletDal _walletDal;
        public AddFundsToWalletCommandHandler(IWalletDal walletDal)
        {
            _walletDal = walletDal;
        }
        public async Task<Result> Handle(AddFundsToWalletCommandRequest request, CancellationToken cancellationToken)
        {
            var wallet = await _walletDal.Get(x => x.UserId == request.Email).Include(x => x.WalletDetails).FirstOrDefaultAsync();
            if (wallet == null)
                return Result.Fail(new SessionNotValidResult());

            wallet.WalletDetails.FirstOrDefault().Amount += request.Amount;
            await _walletDal.UpdateAsync(wallet);
            return Result.Ok();
        }
    }
}
