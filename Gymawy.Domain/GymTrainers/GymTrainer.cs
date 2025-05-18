using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.GymTrainers
{
    public class GymTrainer : BaseEntity
    {
        public GymTrainer(
            Guid trainerId , 
            Guid gymId ,
            DateTime createdOnUtc)
            : base(createdOnUtc)
        {
            GymId = gymId;
            TrainerId = trainerId;  
            CreatedOnUtc = createdOnUtc;    
        }
        protected GymTrainer()
        {
            Id = Guid.NewGuid();
        }
        public Guid TrainerId { get; private set; }
        public Guid GymId { get; private set; }

        public Trainer Trainer { get; private set; }
        public Gym Gym { get; private set; }


    }
}
