using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.Sessions;
using Gymawy.Domain.TrainerInvitations;
using Gymawy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Trainers
{
    public class Trainer : BaseEntity
    {

        public Trainer
            (Guid userId,
            string specialty,
            string certification,
            string bio,
            DateTime createdOnUtc,
            Guid? id = null)
            : base(createdOnUtc, id)
        {
            UserId = userId;
            Specialty = specialty;
            Certification = certification;
            Bio = bio;
            Sessions = new List<Session>();
            Gyms = new List<Gym>();
            TrainerInvitaions = new List<TrainerInvitaion>();
        }
        protected Trainer()
        {
            Sessions = new List<Session>();
            Gyms = new List<Gym>();
            TrainerInvitaions = new List<TrainerInvitaion>();

        }

        public Guid UserId { get; private set; }

        public string Specialty { get; private set; }

        public string Certification { get; private set; }

        public string Bio { get; private set; }

        public User User { get; private set; }

        public ICollection<Session> Sessions { get; private set; }
        public ICollection<Gym> Gyms { get; private set; }

        public ICollection<TrainerInvitaion> TrainerInvitaions { get; private set; }

        public bool HasInvitation (Guid initationId)
        {
            return TrainerInvitaions.Any(x=> x.Id == initationId);  
        }

        public bool IsAvailableForSession(Session session)
        {
            return Sessions.All(s =>
                s.Date != session.Date || (
                    session.EndTime <= s.StartTime || session.StartTime >= s.EndTime
                )
            );
        }


    }
}
