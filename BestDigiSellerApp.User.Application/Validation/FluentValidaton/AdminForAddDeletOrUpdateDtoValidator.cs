using BestDigiSellerApp.User.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.Validation.FluentValidaton
{
    public class AdminForAddDeletOrUpdateDtoValidator : AbstractValidator<AdminForAddDeletOrUpdateDto>
    {
        public AdminForAddDeletOrUpdateDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("The Email must be not null");
        }
    }
}
