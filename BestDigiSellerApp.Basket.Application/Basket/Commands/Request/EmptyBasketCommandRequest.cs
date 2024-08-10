using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Application.Basket.Commands.Request
{
    public class EmptyBasketCommandRequest : IRequest<Result>
    {
    }
}
