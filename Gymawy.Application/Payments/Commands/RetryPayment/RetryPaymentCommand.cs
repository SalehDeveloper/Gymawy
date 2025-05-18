using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Payments.Commands.RetryPayment
{
    public record RetryPaymentCommand ():IRequest<ErrorOr<string>>;
    
    
}
