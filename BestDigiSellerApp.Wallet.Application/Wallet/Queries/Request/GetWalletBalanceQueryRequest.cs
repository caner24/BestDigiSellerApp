using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Application.Wallet.Queries.Request
{
    public record GetWalletBalanceQueryRequest : IRequest<Result<double>>
    {
        public string? Email { get; init; }
    }
}
