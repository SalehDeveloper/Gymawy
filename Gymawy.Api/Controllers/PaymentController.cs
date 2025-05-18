using Gymawy.Application.Payments.Commands;
using Gymawy.Application.Payments.Commands.RetryPayment;
using Gymawy.Application.Payments.Commands.VerifyPayment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymawy.Api.Controllers
{
    public class PaymentController : ApiController
    {


        private readonly ISender _sender;

        public PaymentController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Roles=Roles.Admin)]
        [HttpPost(ApiEndpoints.Payments.Retry)]

        public async Task<IActionResult> Retrypayment ()
        {
            var command = new  RetryPaymentCommand();

            var result = await _sender.Send(command);

            return result.Match(
                v => base.Ok(result.Value),
                Problem);
        }

        [Authorize(Roles=Roles.Admin)]
        [HttpPost(ApiEndpoints.Payments.Verify)]
        public async Task<IActionResult> VerifyPayment(string sessionId)
        {
            var command = new VerifyPaymentCommand(sessionId);

            var result = await _sender.Send(command);

            return result.Match(res => base.Ok(result.Value), Problem);
        }
    }
}
