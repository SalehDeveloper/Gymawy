using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidator :AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x=> x.Email).NotEmpty().EmailAddress();
           
            RuleFor(x=> x.code)
                .NotEmpty()
                .MaximumLength(8)
                .WithMessage("maximum length is 8 numbers")
                .MinimumLength(8)
                .WithMessage("minimum length is 8 numbers");
        }
    }
}
