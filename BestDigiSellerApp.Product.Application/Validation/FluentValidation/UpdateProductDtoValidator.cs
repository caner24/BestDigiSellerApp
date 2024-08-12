using BestDigiSellerApp.Product.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Application.Validation.FluentValidation
{
    public class UpdateProductDtoValidator : AbstractValidator<ProductForUpdateDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.ProductId).NotNull().NotEmpty().WithMessage("ProductId must be not null !.");
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name must be not null and not empty.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Min price must be greater than 0");
            RuleFor(x => x.Stock).GreaterThan(0).WithMessage("Min stock must be greater than 0");
            RuleFor(x => x.PointPercentage).GreaterThan(0).LessThan(101).WithMessage("PointPercentage must be greater than 0");
            RuleFor(x => x.MaxPoint).GreaterThan(0).WithMessage("MaxPoint must be greater than 0");

        }
    }
}
