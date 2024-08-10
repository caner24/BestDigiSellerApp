using BestDigiSellerApp.User.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.Validation.FluentValidaton
{
    public class UserPasswordResetDtoValidator : AbstractValidator<UserPasswordResetDto>
    {
        public UserPasswordResetDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("The Email must be not null");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("The Password must be not null");
            RuleFor(x => x.RePassword).Matches(x => x.Password).WithMessage("The RePassword must match Password null").NotNull().WithMessage("The RePassword must be not null");
            RuleFor(x => x.Token).NotEmpty().NotNull().WithMessage("The Token must be not null");
        }
    }
}
