using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Stripe;
using Gymawy.Domain.Admins;
using Gymawy.Domain.Payments;
using Gymawy.Domain.Subscriptions;
using Gymawy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Payments.Commands.RetryPayment
{
    public class RetryPaymentCommandHandler : IRequestHandler<RetryPaymentCommand, ErrorOr<string>>
    {
        private readonly IPaymentsRepository _paymentRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IStripeService _stripeService;
        private readonly IUserContext _userContext;
        private readonly IUsersRepository _usersRepository;
        private readonly IAdminsRepository _adminsRepository;




        public RetryPaymentCommandHandler(
            IPaymentsRepository paymentRepository,
            ISubscriptionsRepository subscriptionsRepository,
            IStripeService stripeService,
            IUserContext userContext,
            IUsersRepository usersRepository,
            IAdminsRepository adminsRepository)
        {
            _paymentRepository = paymentRepository;
            _subscriptionsRepository = subscriptionsRepository;
            _stripeService = stripeService;
            _userContext = userContext;
            _usersRepository = usersRepository;
            _adminsRepository = adminsRepository;
        }

        public async Task<ErrorOr<string>> Handle(RetryPaymentCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            
            var admin = await _adminsRepository.FindAsync(x => x.UserId == userId, new[] {"Subscription"} , cancellationToken);
            
            var subscripiton = await _subscriptionsRepository.GetByIdAsync(admin.SubscriptionId.Value);

            if (subscripiton == null)
                return SubscriptionErrors.NotFound;

            var sessionUrl = await _stripeService.CreateCheckOutSession(subscripiton.Id, subscripiton.GetPrice(), "usd");

            return sessionUrl;

        }
    }
}
