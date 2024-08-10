using BestDigiSellerApp.Discount.Application.Discount.Commands.Response;
using BestDigiSellerApp.Discount.Application.Discount.Handlers.CommandHandler;
using BestDigiSellerApp.Discount.Entity.Dto;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Application.Discount.Commands.Request
{
    public record CreateCouponCodeCommandRequest : IRequest<Result>
    {
        public DateTime ExpireTime { get; init; }
        public int CouponPercentage { get; init; }

    }
}
