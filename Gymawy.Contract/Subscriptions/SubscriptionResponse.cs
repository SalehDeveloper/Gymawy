using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Contract.Subscriptions
{
    public  record SubscriptionResponse(
        Guid Id , 
        Guid adminId,
        SubscriptionType SubscriptionType , 
        SubscriptionStatus SubscriptionStatus ,
        string? SessionUrl,
        string Message );
    
    
}
