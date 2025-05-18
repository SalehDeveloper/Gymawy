using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Bookings
{
    public class BookingStatus : SmartEnum<BookingStatus>
    {
        public static readonly BookingStatus Pending = new(nameof(Pending), 0);
        public static readonly BookingStatus Confirmed = new(nameof(Confirmed), 1);
        public static readonly BookingStatus Refunded = new(nameof(Refunded), 2);
        public static readonly BookingStatus Failed = new(nameof(Failed), 3);


        public BookingStatus(string name, int value) : base(name, value)
        {
        }
    }
}
