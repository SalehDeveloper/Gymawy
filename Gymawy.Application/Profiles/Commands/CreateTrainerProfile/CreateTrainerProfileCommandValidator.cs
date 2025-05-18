using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Profiles.Commands.CreateTrainerProfile
{
    public class CreateTrainerProfileCommandValidator : AbstractValidator<CreateTrainerProfileCommand>
    {
        public CreateTrainerProfileCommandValidator()
        {
            RuleFor(x => x.Certification).NotEmpty();
            RuleFor(x => x.Bio).NotEmpty();
            RuleFor(x => x.Specialty).NotEmpty();

        }
    }
}
