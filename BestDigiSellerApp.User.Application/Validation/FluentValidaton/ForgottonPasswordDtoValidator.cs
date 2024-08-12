using BestDigiSellerApp.User.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.Validation.FluentValidaton
{
    public class ForgottonPasswordDtoValidator : AbstractValidator<ForgottonPasswordDto>
    {
        public ForgottonPasswordDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("The EmailAdress must be not null");
        }
    }
}
