using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Contract.Bookings
{
    public record BookingResponse(Guid BookingId, Guid SessionId , Guid ParticipantId , DateTime Date , decimal Amount , BookingStatus Status , string Message);
    
    
}
