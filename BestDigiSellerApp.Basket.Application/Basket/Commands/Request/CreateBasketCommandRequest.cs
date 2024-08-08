using BestDigiSellerApp.Basket.Application.Basket.Commands.Response;
using BestDigiSellerApp.Basket.Entity.Dto;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Application.Basket.Commands.Request
{
    public record CreateBasketCommandRequest : ProductRequestDto, IRequest<Result<CreateBasketCommandResponse>>
    {

    }
}
