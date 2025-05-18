using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        { 
            RuleFor(x=> x.GymId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);

        }
    }
}
