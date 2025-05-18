using ErrorOr;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Bookings;
using Gymawy.Domain.Sessions;
using Gymawy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Participants
{
    public class Participant : BaseEntity
    {
        public Participant
            (
            Guid userId ,
            DateTime createdOnUtc,
            Guid? id =null)
            : base(createdOnUtc ,id)
        {
            UserId = userId;
            Sessions = new List<Session>();
            Bookings = new List<Booking>();
        }
        protected Participant()
        {
            Sessions = new List<Session>();
            Bookings = new List<Booking>();
        }


        public Guid UserId { get; private set; }

        public User User { get; private set; }

        public ICollection<Session> Sessions { get; private set; }

        public ICollection<Booking> Bookings { get; private set; }
        public ErrorOr<Booking> SpotSession(Session session , DateTime utcNow)
        {
            if (!CanSpotSession(session))
                return ParticipantErrors.OverlappedSession;

            var booking = new Booking(Id, session.Id, utcNow, 0m, BookingStatus.Pending, utcNow);

            Bookings.Add(booking);

            return booking;

        }

        public bool CanSpotSession (Session session)
        {
            return Sessions.All(s =>
               s.Date != session.Date || (
                   session.EndTime <= s.StartTime || session.StartTime >= s.EndTime
               )
           );
        }

    }
}
