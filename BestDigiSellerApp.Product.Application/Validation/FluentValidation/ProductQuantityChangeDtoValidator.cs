using BestDigiSellerApp.Product.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Application.Validation.FluentValidation
{
    public class ProductQuantityChangeDtoValidator : AbstractValidator<ProductQuantityChangeDto>
    {
        public ProductQuantityChangeDtoValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be not null");
            RuleFor(x => x.ProductId).NotNull().NotEmpty().WithMessage("Product Id must be not null");
        }

    }
}
