using Gymawy.Api.Mappers;
using Gymawy.Application.Bookings.Commands.VerifyBooking;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymawy.Api.Controllers
{
    public class BookingsController : ApiController
    {
        private readonly ISender _sender;

        public BookingsController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Roles = Roles.Participant)]
        [HttpPost(ApiEndpoints.Booking.Verify)]
        public async Task<IActionResult> Verfiy ([FromRoute]string sessionId, CancellationToken cancellationToken)
        {
            var command = new VerifyBookingCommand(sessionId);

            var res = await _sender.Send(command , cancellationToken);

           return res.Match(
                v => base.Ok(res.Value.MapToResponse()),
                Problem);
        }
    }
}
