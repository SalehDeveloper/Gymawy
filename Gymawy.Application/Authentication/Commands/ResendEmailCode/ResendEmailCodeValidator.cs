using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Authentication.Commands.ResendEmailCode
{
    public class ResendEmailCodeValidator :AbstractValidator<ResendEmailCodeCommand>
    {
        public ResendEmailCodeValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("please enter a valid email-address");
        }
    }
}
