using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Application.Abstractions.Email;
using Gymawy.Application.Abstractions.Stripe;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Bookings;
using Gymawy.Domain.Sessions;
using Gymawy.Domain.Subscriptions;
using Gymawy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Bookings.Commands.VerifyBooking
{
    public class VerifyBookingCommandHandler : IRequestHandler<VerifyBookingCommand, ErrorOr<BookingResult>>
    {  
        private readonly IStripeService _stripeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IBookingsRepository _bookingsRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IEmailService _emailService;
        private readonly IUsersRepository _usersRepository;
        public async Task<ErrorOr<BookingResult>> Handle(VerifyBookingCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            
            var  user = await _usersRepository.GetByIdAsync(userId);
           
            var session = await _stripeService.GetSessionAsync(request.SessionId);

            if (session is null)
                return SessionErrors.NotFound;

            if (session.Status == "complete")
            {
                var bookingIdStr = session.ClientReferenceId;
                if (!Guid.TryParse(bookingIdStr, out var bookingId))
                    return Error.Validation(description: "Invalid booking ID in Stripe session.");

                var booking = await _bookingsRepository.FindAsync(x=> x.Id == bookingId , new[] {nameof(Booking.Participant)});


                
                if (booking == null)
                    return Error.NotFound(description: "booking associated with the session was not found.");

                if (booking.Participant.Id != user.ParticipantId)
                    return Error.Unauthorized("You do not own this booking.");

                // set booking as paid 
                booking.SetAsConfimr(_dateTimeProvider.UtcNow, (decimal)session.AmountTotal / 100m);

                await _unitOfWork.CompleteAsync();
                await _emailService.SendBookingConfirmationEmail(user.Email, booking);
                return new BookingResult(booking , "Booing completed successfully. Enjoy with your session! We've sent a confirmation email with the Booking details.");
            }

            return Error.Conflict(description: "The session is not marked as completed.");
        }
    }
}
