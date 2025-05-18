using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Subscriptions
{
    public class SubscriptionStatus : SmartEnum<SubscriptionStatus>
    {
        public static readonly SubscriptionStatus Pending   = new(nameof(Pending), 0);
        public static readonly SubscriptionStatus Active    = new(nameof(Active), 1);
        public static readonly SubscriptionStatus PastDue   = new(nameof(PastDue), 2);
        public static readonly SubscriptionStatus Canceled  = new(nameof(Canceled), 3);
        public static readonly SubscriptionStatus Expired   = new(nameof(Expired), 4);

        public SubscriptionStatus(string name, int value) 
            : base(name, value)
        {
        }


    }
}
