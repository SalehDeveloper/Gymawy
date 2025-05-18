using ErrorOr;
using Gymawy.Domain.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Subscriptions.Commands.CreateSubscription
{
    public record CreateSubscriptionCommand(SubscriptionType SubscriptionType)
        :IRequest<ErrorOr<SubscriptionResult>>;
    
    
}
