
using ErrorOr;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Subscriptions;
using Gymawy.Domain.Users;

namespace Gymawy.Domain.Admins
{
    public class Admin :BaseEntity
    {
        public Admin(
            Guid userId ,
            DateTime createdOnUtc,
            Guid? subscriptionId = null,
            Guid? id =null)
           : base(createdOnUtc , id)
        {
           UserId = userId;
           SubscriptionId = subscriptionId?? null;
        }

        protected Admin()
        {
            
        }

        public Guid UserId { get;private  set; }

        public string? StripeAccountId {  get; private set; }
        public Guid? SubscriptionId { get;private set; }

        public User User { get;private set; }
        public Subscription? Subscription { get;private set; }

        public ErrorOr<Success> SetSubscription(Subscription subscription)
        {
            if (SubscriptionId.HasValue)
                return AdminErrors.AlreadyHasSubscription;

            SubscriptionId = subscription.Id;
            return Result.Success;   
        }

        public ErrorOr<Success> SetStripeAccountId (string stripeAccountId)
        {
            if (!string.IsNullOrEmpty(StripeAccountId))
                return AdminErrors.AlreadyHasStripeAccount;

            StripeAccountId = stripeAccountId;

            return Result.Success;

        }
    }

   
}
