using BestDigiSellerApp.User.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.Validation.FluentValidaton
{
    public class TwoStepLoginDtoValidator : AbstractValidator<LoginTwoStepDto>
    {
        public TwoStepLoginDtoValidator()
        {
            RuleFor(x => x.Code).NotNull().WithMessage("The Code must be not null");
        }
    }
}
