using Gymawy.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Subscriptions.Commands.CreateSubscription
{
    public record SubscriptionResult(Subscription Subscription , string? sessionUrl);
    
    
}
