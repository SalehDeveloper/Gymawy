using Gymawy.Contract.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Contract.Sessions
{
    public record SpotSessionResponse(Guid BookingId , Guid SessionId , Guid participant , BookingStatus Status , DateTime BookingDate , string SessionUrl);
}
