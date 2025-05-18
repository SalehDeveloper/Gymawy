

using Gymawy.Application.Abstractions.Stripe;

using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Stripe.FinancialConnections;
using AccountService = Stripe.AccountService;
using Session = Stripe.Checkout.Session;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using SessionService = Stripe.Checkout.SessionService;

namespace Gymawy.Infrastructure.Stripe
{
    public class StripeService : IStripeService
    {
        private readonly StripeClient _client;
        private readonly StripeOptions _options;    

        public StripeService(IOptions<StripeOptions> options)
        { 
            _options = options.Value;
            _client = new StripeClient(options.Value.SecretKey);
        }

      

        public async  Task<string> CreateCheckOutSession(Guid subscriptionId, decimal price, string currency)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                ClientReferenceId = subscriptionId.ToString(),
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = currency.ToLower() , 
                    UnitAmount = (long)price *100,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Basic Plan",
                    },
                },
                Quantity = 1,
            }
        },
                Mode = "payment", 
                SuccessUrl = _options.SuccessUrl,
                CancelUrl = _options.CancelUrl,
                Metadata = new Dictionary<string, string>
                 {
                 { "subscriptionId", subscriptionId.ToString() }
                  }
            };

            var service = new SessionService(_client);
            Session session = await service.CreateAsync(options);

            return  session.Url;
        }

        public async Task<StripeAccountResponse> CreateStripeAccountLinkForAdmin(Guid adminId, string email)
        {

            var accountOptions = new AccountCreateOptions
            {
                Type = "standard",
                Country = "US", 
                Email = email,
                BusinessType = "individual" 
            };

            var accountService = new AccountService(_client);
            var account = await accountService.CreateAsync(accountOptions);



           
            var linkOptions = new AccountLinkCreateOptions
            {
                Account = account.Id,
                RefreshUrl = _options.RefreshUrl,
                ReturnUrl = _options.ReturnUrl,
                Type = "account_onboarding"
            };

             var accountLinkService = new AccountLinkService(_client);
            var accountLink = await accountLinkService.CreateAsync(linkOptions);


            return new StripeAccountResponse(accountLink.Url , account.Id);
        }

        public async Task<global::Stripe.Checkout.Session> GetSessionAsync(string sessionId)
        {
            var service = new global::Stripe.Checkout.SessionService(_client);
              return await service.GetAsync(sessionId);
        }

        public async Task<string> CreateCheckOutSessionForSpotAsync(Guid bookingId ,  Guid sessionId,  decimal price, string stripeAccountId)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                ClientReferenceId = bookingId.ToString(),
                LineItems = new List<SessionLineItemOptions>
            {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    UnitAmount = (long)(price * 100),
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Session Spot",
                    }
                },
                Quantity = 1,
            }
        },
                PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                   
                    TransferData = new SessionPaymentIntentDataTransferDataOptions
                    {
                        Destination = stripeAccountId 
                    }
                },
                Mode = "payment",
                SuccessUrl = _options.SuccessUrl,
                CancelUrl = _options.CancelUrl,
                 Metadata = new Dictionary<string, string>
                 {
                 { "bookingId", bookingId.ToString() }
                  }
            };

            var service = new SessionService(_client);
            var session = await service.CreateAsync(options);

            return session.Url;
        }
    }

  

        




}