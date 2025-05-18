using ErrorOr;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Subscriptions
{
    public class Subscription:BaseEntity
    {
        public Subscription(
            Guid adminId,
            SubscriptionType type, 
            DateTime createdOnUtc , 
            Guid? id = null)
            :base( createdOnUtc , id)
        {
           Type = type;
           _maxGyms = GetMaxGyms();
           _price = GetPrice();
           Gyms = new List<Gym>();
           Payments = new List<Payment>();
           AdminId = adminId;
           Status = SubscriptionStatus.Pending;
          
        }

        protected Subscription()
        {
            Payments = new List<Payment>();
            Gyms = new List<Gym>();
        }
        
        private readonly decimal _price;
        private readonly int _maxGyms;
        public SubscriptionType Type { get; private set; }
        
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public SubscriptionStatus Status { get; private set; }
        public Admin Admin { get; private set; }
        public Guid AdminId { get; private set; }
        public ICollection<Gym> Gyms { get; private set; }
        public ICollection<Payment> Payments { get; private set; }   
       
        public int GetMaxGyms() => Type.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 1,
            nameof(SubscriptionType.Pro) => 3,
            _ => throw new InvalidOperationException()

        };
       
        public decimal GetPrice() => Type.Name switch
        {

            nameof(SubscriptionType.Free) => 0m,
            nameof(SubscriptionType.Starter) => 20m,
            nameof(SubscriptionType.Pro) => 45m,
            _ => throw new InvalidOperationException()
        };

        public int GetMaxRooms() => Type.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 4,
            nameof(SubscriptionType.Pro) => int.MaxValue,
            _ => throw new InvalidOperationException()

        };

        public int GetMaxDailySessions() => Type.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => int.MaxValue,
            nameof(SubscriptionType.Pro) => int.MaxValue,
            _ => throw new InvalidOperationException()

        };

        public void Activate(DateTime start , DateTime? end , DateTime updatedAt)
        {
            StartDate = start;
            EndDate = end?? null;
           
            Status= SubscriptionStatus.Active;
            UpdatedAt = updatedAt;
        }
        
        public void Cancel (DateTime utcNow)
        {

            Status = SubscriptionStatus.Canceled;
            EndDate= utcNow;
            UpdatedAt = utcNow;
        }
        public void MarkPaymentFailed (DateTime utcNow)
        {
            Status = SubscriptionStatus.PastDue;
            UpdatedAt= utcNow;
        }
        public void AddPayment(Payment payment , DateTime updatedAt)
        {
            Payments.Add(payment);
            UpdatedAt = updatedAt;
        }
        public bool IsValid (DateTime utcNow)
        {
            if ( Status==SubscriptionStatus.Active && EndDate > utcNow)
                return true;
            return false;
        }
        public ErrorOr<Success> AddGym (Gym gym)
        {
            if (Gyms.Count >= GetMaxGyms())
                return SubscriptionErrors.MaxGymLimitReached;

            Gyms.Add(gym);

            return Result.Success;
        }

        public bool HasGym(Guid gymId)
        {
            return Gyms.Any(x=> x.Id == gymId);    
        }

      
    }
}
