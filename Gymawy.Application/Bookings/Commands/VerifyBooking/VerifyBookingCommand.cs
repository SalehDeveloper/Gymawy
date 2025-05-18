
using ErrorOr;
using Gymawy.Domain.Bookings;
using MediatR;

namespace Gymawy.Application.Bookings.Commands.VerifyBooking
{
    public record VerifyBookingCommand(string SessionId):IRequest<ErrorOr<BookingResult>>;
}
