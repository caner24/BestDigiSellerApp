using BestDigiSellerApp.Product.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Application.Validation.FluentValidation
{
    public class DeleteProductDtoValidator : AbstractValidator<ProductForDeleteDto>
    {
        public DeleteProductDtoValidator()
        {
            RuleFor(x => x.ProductId).NotNull().NotEmpty().WithMessage("ProductId must be not null !.");
        }
    }
}
