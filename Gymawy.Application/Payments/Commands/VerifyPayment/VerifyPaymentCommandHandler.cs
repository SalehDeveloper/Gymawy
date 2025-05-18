using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Application.Abstractions.Email;
using Gymawy.Application.Abstractions.Stripe;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Payments;
using Gymawy.Domain.Subscriptions;
using Gymawy.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Payments.Commands.VerifyPayment
{
    public class VerifyPaymentCommandHandler : IRequestHandler<VerifyPaymentCommand, ErrorOr<string>>
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStripeService _stripeService;
        private readonly IEmailService _emailService;
        private readonly IUserContext _userContext;
        private readonly IUsersRepository _usersRepository;



        public VerifyPaymentCommandHandler(
            IPaymentsRepository paymentsRepository,
            ISubscriptionsRepository subscriptionsRepository,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork,
            IStripeService stripeService,
            IEmailService emailService,
            IUsersRepository usersRepository,
            IUserContext userContext)
        {
            _paymentsRepository = paymentsRepository;
            _subscriptionsRepository = subscriptionsRepository;
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;

            _stripeService = stripeService;
            _emailService = emailService;
            _usersRepository = usersRepository;
            _userContext = userContext;
        }

        public async Task<ErrorOr<string>> Handle(VerifyPaymentCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var user = await _usersRepository.GetByIdAsync(userId);

            var session = await _stripeService.GetSessionAsync(request.SessionId);

            if (session == null)
                return Error.NotFound(description: "Stripe session was not found.");


            if (session.Status == "complete")
            {
                var subscriptionIdStr = session.ClientReferenceId;
                if (!Guid.TryParse(subscriptionIdStr, out var subscriptionId))
                    return Error.Validation(description: "Invalid subscription ID in Stripe session.");

                var subscription = await _subscriptionsRepository.GetByIdAsync(subscriptionId);
                if (subscription == null)
                    return Error.NotFound(description: "Subscription associated with the session was not found.");



                var payment = new Payment(
                    subscriptionId,
                    (decimal)(session.AmountTotal / 100m),
                session.Currency,
                PaymentStatus.Paid,
                _dateTimeProvider.UtcNow ,
                _dateTimeProvider.UtcNow
                );
                subscription.Activate(_dateTimeProvider.UtcNow, _dateTimeProvider.UtcNow.AddMonths(1), _dateTimeProvider.UtcNow);
                subscription.AddPayment(payment, _dateTimeProvider.UtcNow);

                await _paymentsRepository.AddAsync(payment);
                await _subscriptionsRepository.Update(subscription);
                await _unitOfWork.CompleteAsync();
                await _emailService.SendPaymentConfirmationEmail(user.Email, payment);
                return "Payment completed successfully. Enjoy your subscription! We've sent a confirmation email with the payment details.";
            }
            return Error.Conflict(description: "The session is not marked as completed.");


        }
    }
}
