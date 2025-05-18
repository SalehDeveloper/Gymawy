using Gymawy.Domain.Bookings;
using Gymawy.Domain.Payments;
using Gymawy.Domain.Sessions;
using Gymawy.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Abstractions.Email
{
    public interface IEmailService
    {
        public Task SendConfirmationToEamilAsync(string toEmail, string subject, string title, string code, string time);
        Task SendPaymentConfirmationEmail(string email, Payment payment);
        Task SendBookingConfirmationEmail(string email, Booking booking);
        Task SendPaymentFailedEmail(string email, Payment payment);
        Task SendSubscriptionExpiringSoonEmail(string email, string subscriptionType, DateTime endDate);
        Task SendSubscriptionExpiredEmail(string email, string subscriptionType);
        Task SendSubscriptionCancelledEmail(string email, Subscription subscription);
        Task NotifyTrainerForSession (string email , Session session);
    }
}
