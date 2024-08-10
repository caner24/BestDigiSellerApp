using BestDigiSellerApp.Basket.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Application.Validaton
{
    public class DeleteItemToBasketDtoValidator:AbstractValidator<DeleteItemToBasketDto>
    {
        public DeleteItemToBasketDtoValidator()
        {
            RuleFor(x => x.ProductId).NotNull().NotEmpty().WithMessage("The productId must be not null");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("The Quantity must be greater than 0");
        }
    }
}
