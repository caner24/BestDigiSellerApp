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
    public record ValidateCouponCodeCommandRequest : ValidateCouponCodeDto, IRequest<Result<int>>
    {

    }
}
