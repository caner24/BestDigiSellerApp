using BestDigiSellerApp.Basket.Entity;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Application.Basket.Queries.Request
{
    public record GetUserBasketQueryRequest:IRequest<Result<ShoppingCart>>
    {

    }
}
