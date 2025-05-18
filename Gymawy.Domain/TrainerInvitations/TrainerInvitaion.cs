using ErrorOr;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.TrainerInvitations
{
    public class TrainerInvitaion:BaseEntity
    {
        public static int InviationExpiredInDays = 3;
        public Guid GymId { get; private set; }
        public Guid TrainerId {  get; private set; }    
        public InvitationStatus Status { get; private set; }
        public DateTime ExpirationDateUtc { get; private set; }

        public Gym Gym { get; private set; }
        public Trainer Trainer { get; private set; }

        public TrainerInvitaion(
            Guid gymId,
            Guid trainerId,
            InvitationStatus status,
            DateTime expirationDateUtc,
            DateTime createdOnutc)
            :base(createdOnutc)
        {
            GymId = gymId;
            TrainerId = trainerId;
            Status = status;
            ExpirationDateUtc = expirationDateUtc;
        }

        protected TrainerInvitaion()
        {
            
        }

        public bool IsExpired (DateTime utcNow)
        {
            return ExpirationDateUtc < utcNow;
        }

        public ErrorOr<Success> Accept (DateTime utcNow)
        {
            if (IsExpired(utcNow))
                return TrainerInvitationErrors.Expired;

            if (Status !=  InvitationStatus.Pending)
                return TrainerInvitationErrors.InvalidState;

            Status = InvitationStatus.Accepted;
            UpdatedAt = utcNow;

            return Result.Success;
        }

        public ErrorOr<Success> Reject(DateTime utcNow)
        {
            if (IsExpired(utcNow))
                return TrainerInvitationErrors.Expired;

            if (Status != InvitationStatus.Pending)
                return TrainerInvitationErrors.InvalidState;

            Status = InvitationStatus.Rejected;
            UpdatedAt = utcNow;

            return Result.Success;
        }


    }
}
