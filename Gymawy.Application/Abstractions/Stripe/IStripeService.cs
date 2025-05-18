
using Stripe;
using Stripe.Checkout;



namespace Gymawy.Application.Abstractions.Stripe
{
    public interface  IStripeService
    {

        Task<string> CreateCheckOutSessionForSpotAsync(Guid bookingId, Guid sessionId, decimal price, string stripeAccountId);
        Task<Session> GetSessionAsync(string sessionId);

        Task<StripeAccountResponse> CreateStripeAccountLinkForAdmin(Guid adminId, string email);

         Task<string> CreateCheckOutSession(Guid subscriptionId, decimal price, string currency);
    }
}
