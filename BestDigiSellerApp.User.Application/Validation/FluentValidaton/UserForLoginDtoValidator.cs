using BestDigiSellerApp.User.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.Validation.FluentValidaton
{
    public class UserForLoginDtoValidator : AbstractValidator<UserForLoginDto>
    {
        public UserForLoginDtoValidator()
        {
            RuleFor(x => x.IsRemember).NotNull().WithMessage("The IsRemember must be not null");
            RuleFor(x => x.Password).NotNull().WithMessage("The Password must be not null");
            RuleFor(x => x.UserEmail).NotNull().WithMessage("The UserEmail must be not null");
        }
    }
}
