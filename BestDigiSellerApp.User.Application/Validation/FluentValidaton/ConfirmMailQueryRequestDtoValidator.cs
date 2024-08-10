using BestDigiSellerApp.User.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.Validation.FluentValidaton
{
    public class ConfirmMailQueryRequestDtoValidator : AbstractValidator<ConfirmMailQueryRequestDto>
    {
        public ConfirmMailQueryRequestDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("The Email must be not null");
            RuleFor(x => x.Token).NotNull().WithMessage("The Token must be not null");
        }
    }
}
