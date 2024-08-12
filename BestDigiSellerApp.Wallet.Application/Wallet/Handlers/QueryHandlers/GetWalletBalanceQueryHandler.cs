using BestDigiSellerApp.Wallet.Application.Wallet.Commands.Request;
using BestDigiSellerApp.Wallet.Application.Wallet.Commands.Response;
using BestDigiSellerApp.Wallet.Application.Wallet.Queries.Request;
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

namespace BestDigiSellerApp.Wallet.Application.Wallet.Handlers.QueryHandlers
{
    public class GetWalletBalanceQueryHandler : IRequestHandler<GetWalletBalanceQueryRequest, Result<double>>
    {
        private readonly IWalletDal _walletDal;
        public GetWalletBalanceQueryHandler(IWalletDal walletDal)
        {
            _walletDal = walletDal;
        }

        async Task<Result<double>> IRequestHandler<GetWalletBalanceQueryRequest, Result<double>>.Handle(GetWalletBalanceQueryRequest request, CancellationToken cancellationToken)
        {
            var wallet = await _walletDal.Get(x => x.UserId == request.Email).Include(x => x.WalletDetails).Select(x => x.WalletDetails).FirstOrDefaultAsync();
            if (wallet == null)
                return Result.Fail(new SessionNotValidResult());

            var cashback = wallet.Select(x => x.Amount).FirstOrDefault();
            return Result.Ok(cashback);
        }
    }
}
