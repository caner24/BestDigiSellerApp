using BestDigiSellerApp.Product.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Application.Validation.FluentValidation
{
    public class UpdateCategoryDtoValidator:AbstractValidator<CategoryForUpdateDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id must be not null");
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name must be not null");
            RuleFor(x => x.Tag).NotEmpty().NotNull().WithMessage("Tag must be not null");
        }
    }
}
