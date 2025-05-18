using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Payments.Commands.VerifyPayment
{
    public record VerifyPaymentCommand (string SessionId):IRequest<ErrorOr<string>>;
    
    
}
