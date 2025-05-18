using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Gyms.Commands.CreateGym
{
    public class CreateGymCommandValidator : AbstractValidator<CreateGymCommand>
    {
        public CreateGymCommandValidator()
        {
            RuleFor(x=> x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
