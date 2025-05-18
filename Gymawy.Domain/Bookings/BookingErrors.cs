using Ardalis.SmartEnum;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Bookings
{
    public class BookingErrors : SmartEnum<BookingErrors>
    {
        public static readonly Error InvalidState =
            Error.Conflict(
                code: "Booking.InvalidState",
                description: "booking invalid state");


        public static readonly Error NotFound =
            Error.Validation(
                code: "Booking.NotFound",
                description: "booking not found ");


        public BookingErrors(string name, int value) 
            : base(name, value)
        {
        }
    }
}
