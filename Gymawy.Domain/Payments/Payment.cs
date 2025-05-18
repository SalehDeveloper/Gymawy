using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Bookings;
using Gymawy.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Payments
{
    public class Payment : BaseEntity
    {

        public Guid SubscriptionId { get; private set; }
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        public PaymentStatus Status { get; private set; }
        public DateTime? PaidDate { get; private set; }
        public Subscription Subscription { get; private set; }

        public Payment(
            Guid subscriptionId,
            decimal amount , 
            string currecny, 
            PaymentStatus paymentStatus , 
            DateTime createdOnUtc,
            DateTime paidDate , 
            Guid? id  = null
            ):base(createdOnUtc , id)   
        {
            
            SubscriptionId = subscriptionId;
            Amount = amount;
            Currency = currecny;
            Status = paymentStatus;
            CreatedOnUtc = createdOnUtc;   
           PaidDate = paidDate;

        }
        protected Payment()
        {
            

        }
        public void MarkAsPaid (DateTime utcNow )
        {
            Status = PaymentStatus.Paid;
            PaidDate = utcNow;
            UpdatedAt = utcNow;
        }

        public void MarkAsFailed (DateTime utcNow )
        {
            Status = PaymentStatus.Failed;
            UpdatedAt = utcNow ;
        }
    }
}
