using CloudinaryDotNet.Actions;
using Gymawy.Api.Mappers;
using Gymawy.Application.Subscriptions.Commands.CreateSubscription;
using Gymawy.Contract.Subscriptions;
using Gymawy.Domain.ProfileTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Gymawy.Api.Controllers
{
    
    public class SubscriptionsController : ApiController
    {

        private readonly ISender _sender;

        public SubscriptionsController(ISender sender)
        {
            _sender = sender;
           
        }

        [Authorize(Roles =Roles.Admin)]
        [HttpPost(ApiEndpoints.Subscription.CreateSubscription)]
        public async Task<IActionResult> CreateSubscription ([FromBody] CreateSubscriptionRequest request , CancellationToken cancellationToken)
        {
            if (!Domain.Subscriptions.SubscriptionType.TryFromName(
               request.SubscriptionType , 
               out var subscriptionType)
                )
        
                return Problem("Invalid subscription type", statusCode: StatusCodes.Status400BadRequest);
            


            var command = new CreateSubscriptionCommand(subscriptionType);

            var result = await _sender.Send(command, cancellationToken);

          return  result.Match
                (
                res => base.Ok(result.Value.MapToSubscriptionResponse()),
                Problem
                );
        }

    }
}

