using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Subscriptions
{
    public class SubscriptionErrors
    {
        public static readonly Error NotFound = Error.NotFound
            (
            code: "Subscription.NotFound",
            description: "subscription not found"
            );


        public static readonly Error MaxGymLimitReached = Error.Validation(
        code: "Gym.MaxLimit",
        description: "The maximum number of gyms allowed by your subscription has been reached."
          );

    }
}
