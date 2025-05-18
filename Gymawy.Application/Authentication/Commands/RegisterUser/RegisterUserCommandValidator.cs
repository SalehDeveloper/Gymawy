using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommandValidator: AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x=> x.Email).NotEmpty().EmailAddress();

            RuleFor(x=> x.FullName).NotEmpty().MaximumLength(50);

            RuleFor(x => x.Photo).NotEmpty();

            RuleFor(x => x.Password)
              .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
              .WithMessage("Password too weak");

        }
    }
}
