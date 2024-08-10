using BestDigiSellerApp.User.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.Validation.FluentValidaton
{
    public class UserForRegistrationDtoValidator : AbstractValidator<UserForRegistrationDto>
    {
        public UserForRegistrationDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("The Password must be not null");
            RuleFor(x => x.Password).NotNull().WithMessage("The Password must be not null");
            RuleFor(x => x.UserName).NotNull().WithMessage("The UserName must be not null");
            RuleFor(x => x.PhoneNumber)
                .NotNull().WithMessage("The PhoneNumber must not be null")
                .Matches(@"^\+90\d{10}$").WithMessage("The PhoneNumber must be a valid Turkish number in the format +90XXXXXXXXXX");
        }
    }
}
