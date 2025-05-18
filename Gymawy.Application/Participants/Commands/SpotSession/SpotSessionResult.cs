using Gymawy.Domain.Bookings;

namespace Gymawy.Application.Participants.Commands.SpotSession
{
    public record SpotSessionResult(Booking Booking , string SessionUrl);
   
}
