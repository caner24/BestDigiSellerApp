using BestDigiSellerApp.Basket.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Application.Validaton
{
    public class BasketForCreateDtoValidator : AbstractValidator<BasketForCreateDto>
    {
        public BasketForCreateDtoValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("The ) must be greater than 0");
            RuleFor(x => x.ImageFile).NotNull().NotEmpty().WithMessage("The ImageFile must be not null");
            RuleFor(x => x.ProductName).NotNull().NotEmpty().WithMessage("The ProductName must be not null");
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("The UserName must be not null");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("The Quantity must be greater than 0");
            RuleFor(x => x.PointPercentage).GreaterThan(0).WithMessage("The PointPercentage must be greater than 0");
            RuleFor(x => x.MaxPoint).GreaterThan(0).WithMessage("The MaxPoint must be greater than 0");
        }
    }
}
