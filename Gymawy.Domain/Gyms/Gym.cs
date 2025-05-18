using ErrorOr;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.GymTrainers;
using Gymawy.Domain.Rooms;
using Gymawy.Domain.Subscriptions;
using Gymawy.Domain.TrainerInvitations;
using Gymawy.Domain.Trainers;
namespace Gymawy.Domain.Gyms
{
    public class Gym :BaseEntity
    {
        public Gym( 
            Guid subscriptionId , 
            string name ,
            int maxRooms , 
            DateTime createdOnUtc) 
            : base(createdOnUtc)
        {
            SubscriptionId = subscriptionId;
            Name = name;
            MaxRooms = maxRooms;
            Rooms = new List<Room>();
            Trainers = new List<Trainer>();
            TrainerInvitaions = new List<TrainerInvitaion>();
            GymTrainers = new List<GymTrainer>();
        }
        protected Gym()
        {
            Rooms = new List<Room>();
            Trainers = new List<Trainer>();
            TrainerInvitaions = new List<TrainerInvitaion>();
            GymTrainers = new List<GymTrainer>();
        }
        public string Name { get; private set; } 
        
        public  int MaxRooms { get; private set; }

        public Guid SubscriptionId {  get; private set; }

        public Subscription Subscription { get; private set; }  

        public ICollection<Room> Rooms { get; private set; }
        public ICollection<Trainer> Trainers { get; private set; }
        public ICollection<TrainerInvitaion> TrainerInvitaions { get; private set; }
        public ICollection<GymTrainer> GymTrainers { get; private set; }    
        public ErrorOr<Success> AddRoom (Room room)
        {
            if (Rooms.Count >= MaxRooms)
                return GymErrors.MaxRoomLimitReached;

            Rooms.Add(room);

            return Result.Success;
        }
        public ErrorOr<Success> AssignTrainer (Trainer trainer)
        {
            if (HasTrainer(trainer.Id))
                return GymTrainerErrors.AlreadyAssigned;
            
            Trainers.Add(trainer);
            
            return Result.Success;
        }
        public bool HasTrainer (Guid trainerId)
        {
            return GymTrainers.Any(x=> x.TrainerId == trainerId);
        }
        public bool IsTrainerAlreadyInvited(Guid trainerId)
        {
            return TrainerInvitaions.Any(x => x.TrainerId == trainerId && x.Status == InvitationStatus.Pending);
        }
        public ErrorOr<TrainerInvitaion> InviteTrainer (Trainer trainer , DateTime utcNow)
        {
            if (HasTrainer(trainer.Id))
                return GymTrainerErrors.AlreadyAssigned;

            if (IsTrainerAlreadyInvited(trainer.Id))
                return TrainerInvitationErrors.AlreadyInvited;

            var invitationTrainer = new TrainerInvitaion(Id, trainer.Id, InvitationStatus.Pending, utcNow.AddDays(TrainerInvitaion.InviationExpiredInDays), utcNow);

            TrainerInvitaions.Add(invitationTrainer);   

            return invitationTrainer;
            
        }


    }
}
