using BestDigiSellerApp.Discount.Application.Discount.Commands.Request;
using BestDigiSellerApp.Discount.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Application.Validation.FluentValidaton
{
    public class CreateCouponCodeDtoValidator : AbstractValidator<CreateCouponCodeCommandRequest>
    {
        public CreateCouponCodeDtoValidator()
        {
            RuleFor(x => x.CouponPercentage).NotEmpty().NotNull().WithMessage("The CouponPercentage must be not null");
            RuleFor(x => x.ExpireTime).NotEmpty().NotNull().WithMessage("The ExpireTime must be not null");
        }
    }
}
