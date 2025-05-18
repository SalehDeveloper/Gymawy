using FluentValidation;
using Gymawy.Domain.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Sessions.Commands.CreateSession
{
    public class CreateSessionCommandValidator :AbstractValidator<CreateSessionCommand>
    {
        public CreateSessionCommandValidator()
        {
            RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("Room ID is required.");

            RuleFor(x => x.TrainerId)
                .NotEmpty().WithMessage("Trainer ID is required.");

            RuleFor(x => x.Type)
                .Must(type => SessionType.List.Contains(type))
                .WithMessage("Invalid session type.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.MaxParticipants)
                .GreaterThan(0).WithMessage("Maximum participants must be greater than 0.");

            RuleFor(x => x.Date)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow.Date))
                .WithMessage("Session date cannot be in the past.");

            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime)
                .WithMessage("Start time must be earlier than end time.");
            RuleFor(x => x)
            .Must(x =>
                 {
                    var sessionDateTime = x.Date.ToDateTime(x.StartTime);
                    return sessionDateTime >= DateTime.UtcNow;
                 })
    .WithMessage("Session start time cannot be in the past.");


            RuleFor(x => x.SessionFee)
                .GreaterThanOrEqualTo(0).WithMessage("Session fee cannot be negative.");
        }
    }
}
