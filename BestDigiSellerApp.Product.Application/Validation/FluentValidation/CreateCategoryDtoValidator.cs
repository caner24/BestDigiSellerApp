using BestDigiSellerApp.Product.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Application.Validation.FluentValidation
{
    public class CreateCategoryDtoValidator : AbstractValidator<CategoryForCreateDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name must be not null and not empty.");
            RuleFor(x => x.Tag).NotEmpty().NotNull().WithMessage("Tag must be not null and not empty.");
        }


    }
}
