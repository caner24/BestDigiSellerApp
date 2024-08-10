using BestDigiSellerApp.Discount.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Application.Validation.FluentValidaton
{
    public class ValidateCouponCodeDtoValidator : AbstractValidator<ValidateCouponCodeDto>
    {
        public ValidateCouponCodeDtoValidator()
        {
            RuleFor(x => x.CouponCode).NotEmpty().NotNull().WithMessage("The CouponCode must be not null");
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("The Email must be not null");
        }
    }

}
