using ErrorOr;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Participants;
using Gymawy.Domain.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Bookings
{
    public class Booking : BaseEntity
    {
        public Booking(
            Guid participantId , 
            Guid sessionId , 
            DateTime bookingDate , 
            decimal AmountPaid ,
            BookingStatus paymentStatus ,
            DateTime createdOnUtc)
            : base(createdOnUtc)
        {
            ParticipantId = participantId;
            SessionId = sessionId;
            BookingDate = bookingDate;
            AmountPaid = AmountPaid; 
            Status = paymentStatus;
            CreatedOnUtc = createdOnUtc;
        }

        protected Booking()
        {
            
        }
        public DateTime BookingDate {  get; private set; }

        public BookingStatus Status { get; private set; }

        public decimal AmountPaid {  get; private set; }

        public Guid ParticipantId { get; private set; } 
        public Guid SessionId { get; private set; } 

        public Participant Participant { get; private set; }
        public Session Session { get; private set; }


        public ErrorOr<Success> SetAsConfimr(DateTime utcNow , decimal amount)
        {
            if (Status != BookingStatus.Pending)
                return BookingErrors.InvalidState;
                
                Status = BookingStatus.Confirmed;
                UpdatedAt = utcNow; 
                AmountPaid += amount;

            return Result.Success;

            
        }

     




    }
}
