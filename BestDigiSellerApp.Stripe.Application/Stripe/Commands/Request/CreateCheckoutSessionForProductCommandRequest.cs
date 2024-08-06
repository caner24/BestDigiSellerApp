using BestDigiSellerApp.Stripe.Entity.Dto;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Application.Stripe.Commands.Request
{
    public record CreateCheckoutSessionForProductCommandRequest: CreateCheckoutSessionForProductDto,IRequest<Result<string>>
    {

    }
}
