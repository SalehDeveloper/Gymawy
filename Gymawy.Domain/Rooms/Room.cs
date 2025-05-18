using ErrorOr;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.GymTrainers;
using Gymawy.Domain.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Rooms
{
    public class Room : BaseEntity
    {
        public string Name { get; private set; }
        public int MaxDailySessions { get; private set; }
        public Guid GymId { get; private set; }
        public Gym Gym { get; private set; }
        public ICollection<Session> Sessions { get; private set; }


        protected Room()
        {
            Sessions = new List<Session>();
        }
        public Room(
            string name ,
            int maxDailySessions,
            Guid gymId , 
            DateTime createdOnUtc) 
            : base(createdOnUtc)
        {
            Name = name;
            MaxDailySessions = maxDailySessions;
            GymId = gymId;
            Sessions = new List<Session>();
        }

     

       public ErrorOr<Success> AddSession(Session session)
        {
            if (Sessions.Count(x=> x.Date == session.Date)>= MaxDailySessions)
                return RoomErrors.MaxSessionLimitReached;

            if (HasOverlappingSession(session))
                return RoomErrors.OverlappedSession;


            Sessions.Add(session);

            return Result.Success;
        }

        public bool HasOverlappingSession (Session session)
        {
            return Sessions.Any(s =>
             s.Date == session.Date &&
            (
                 (session.StartTime >= s.StartTime && session.StartTime < s.EndTime) ||
                 (session.EndTime > s.StartTime && session.EndTime <= s.EndTime) ||
                 (session.StartTime <= s.StartTime && session.EndTime >= s.EndTime)
            )
   );
        }

    }
}
