
using BestDigiSellerApp.Wallet.Application.Wallet.Commands.Response;
using BestDigiSellerApp.Wallet.Entity.Dto;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Application.Wallet.Commands.Request
{
    public record CreateWalletCommandRequest : CreateWalletDto, IRequest<Result<CreateWalletCommandResponse>>
    {

    }
}
