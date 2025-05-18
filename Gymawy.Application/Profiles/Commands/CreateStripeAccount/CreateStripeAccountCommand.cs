using ErrorOr;
using Gymawy.Application.Abstractions.Stripe;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Profiles.Commands.CreateStripeAccount
{
    public record CreateStripeAccountCommand():IRequest<ErrorOr<StripeAccountResponse>>;
    
    
}
