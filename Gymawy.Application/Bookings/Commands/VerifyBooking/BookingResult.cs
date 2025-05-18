using Gymawy.Domain.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Bookings.Commands.VerifyBooking
{
    public record BookingResult(Booking Booking , string message);
    
}
