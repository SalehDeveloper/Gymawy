using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Authentication.Commands.Login
{
    public class LoginCommandValidatior : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidatior()
        { 
            RuleFor( x=> x.Email ).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();

        }
    }
}
