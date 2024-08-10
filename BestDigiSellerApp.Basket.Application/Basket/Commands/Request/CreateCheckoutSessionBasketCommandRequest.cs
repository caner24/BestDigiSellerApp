using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Application.Basket.Commands.Request
{
    public record CreateCheckoutSessionBasketCommandRequest : IRequest<Result<string>>
    {
        public string? CouponCode { get; init; }
    }
}
