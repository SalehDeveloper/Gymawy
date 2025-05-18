using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Application.Abstractions.Stripe;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using Gymawy.Domain.Payments;
using Gymawy.Domain.Subscriptions;
using Gymawy.Domain.Users;
using MediatR;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;


namespace Gymawy.Application.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<SubscriptionResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserContext _userContext;
        private readonly IAdminsRepository _adminsRepository;
        private readonly IStripeService _stripeService;
        private readonly StripeOptions _stripeOptions;
        private readonly IPaymentsRepository _paymentsRepository;



        public CreateSubscriptionCommandHandler(
            IUnitOfWork unitOfWork,
            ISubscriptionsRepository subscriptionsRepository,
            IUsersRepository usersRepository,
            IDateTimeProvider dateTimeProvider,
            IUserContext userContext,
            IAdminsRepository adminsRepository,
            IStripeService stripeService,
            IOptions<StripeOptions> stripeOptions,
            IPaymentsRepository paymentsRepository)
        {
            _unitOfWork = unitOfWork;
            _subscriptionsRepository = subscriptionsRepository;
            _usersRepository = usersRepository;
            _dateTimeProvider = dateTimeProvider;
            _userContext = userContext;
            _adminsRepository = adminsRepository;
            _stripeService = stripeService;
            _stripeOptions = stripeOptions.Value;
            _paymentsRepository = paymentsRepository;
        }

        public async Task<ErrorOr<SubscriptionResult>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
           var userId = _userContext.UserId;

           var admin =await _adminsRepository.FindAsync(x=> x.UserId == userId , new[] {"User"}  , cancellationToken);

            if (admin is null)
                return UserErrors.NotFound;


            var subscription = new Domain.Subscriptions.Subscription(admin.Id, request.SubscriptionType, _dateTimeProvider.UtcNow);

            var adminSubscription = admin.SetSubscription(subscription);


            if (adminSubscription.IsError)
                return adminSubscription.Errors;


            if (subscription.Type.Name == SubscriptionType.Free.Name)
            {
                subscription.Activate(_dateTimeProvider.UtcNow, null, _dateTimeProvider.UtcNow);
                
                await _subscriptionsRepository.AddAsync(subscription);
                
                await  _adminsRepository.Update(admin);

                await _unitOfWork.CompleteAsync();
                
                return new SubscriptionResult(subscription, null);
            }
        
            

                var sessionUrl = await _stripeService.CreateCheckOutSession( subscription.Id, subscription.GetPrice(), "usd");

                   await _subscriptionsRepository.AddAsync(subscription);

                    await _adminsRepository.Update(admin);
    
                  await _unitOfWork.CompleteAsync();

                  return new SubscriptionResult(subscription , sessionUrl);
                
              
            

               

            
        }

 
        
    }
}
