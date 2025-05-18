using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Participants;
using Gymawy.Domain.Rooms;
using Gymawy.Domain.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Sessions
{
    public class Session : BaseEntity
    {
        public SessionType Type { get; private set; }

        public string Description { get; private set; }

        public int MaxParticipants {  get; private set; }
        public DateOnly Date {  get; private set; }

        public TimeOnly StartTime { get; private set; } 

        public TimeOnly EndTime { get; private set; }

        public decimal SessionFee { get; private set; }

        public SessionStatus Status { get; private set; }   

        // 
        public Guid RoomId { get; private set; }
        public Guid TrainerId {  get; private set; }

        public Session
            (Guid roomId , 
            Guid trainerId ,
            SessionType type, 
            string description,
            int maxParticipants,
            DateOnly date,
            TimeOnly startTime,
            TimeOnly endTime,
            decimal sessionFee,
            DateTime createdOnUtc,
            SessionStatus? status =null) : base(createdOnUtc)
        {
            RoomId = roomId;
            TrainerId = trainerId;
            Type = type;
            Description = description;
            MaxParticipants = maxParticipants;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            SessionFee = sessionFee;
            CreatedOnUtc = createdOnUtc;
            Status = status ?? SessionStatus.CommingSoon;
            Participants = new List<Participant>();

        }

        protected Session()
        {
            Participants = new List<Participant>();

        }
        public Room Room { get; private set; }
        public Trainer Trainer { get; private set; }

        public ICollection<Participant> Participants { get; private set; }
        
        public bool IsFull()
        {
            return Participants.Count >= MaxParticipants;
        }

      
    }
}
