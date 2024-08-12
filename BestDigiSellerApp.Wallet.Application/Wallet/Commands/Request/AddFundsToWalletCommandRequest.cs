using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Application.Wallet.Commands.Request
{
    public record AddFundsToWalletCommandRequest : IRequest<Result>
    {
        public double Amount { get; init; }
        public string? Email { get; init; }
    }
}
