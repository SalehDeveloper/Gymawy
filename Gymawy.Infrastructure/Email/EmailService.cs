using Gymawy.Application.Abstractions.Email;
using Gymawy.Domain.Bookings;
using Gymawy.Domain.Payments;
using Gymawy.Domain.Sessions;
using Gymawy.Domain.Subscriptions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Gymawy.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;

        public EmailService(IOptions< EmailOptions >emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }
        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Gymawy", _emailOptions.From));

            emailMessage.To.Add(MailboxAddress.Parse(toEmail));

            emailMessage.Subject = subject;


            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using (var client = new SmtpClient())

            {
                try
                {
                    client.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    client.Connect(_emailOptions.SmtpServer, _emailOptions.Port, true);

                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    client.Authenticate(_emailOptions.UserName, _emailOptions.Password);

                    client.Send(emailMessage);



                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();

                }
            }
        }

        public async Task SendConfirmationToEamilAsync(string toEmail, string subject, string title, string code, string time)
        {
            var messageBody = MessageBodyGenerator.GenerateEmailMessageStructure(title, code, time);
            await SendEmailAsync(toEmail, subject, messageBody);


        }

        public async Task SendPaymentConfirmationEmail(string email, Payment payment)
        {
            var subject = $"Payment Confirmation for {payment.Subscription.Type.Name} Subscription";
            var body = $@"
                 <h1>Thank you for your payment!</h1>
                 <p>We've successfully processed your payment of {payment.Amount} {payment.Currency}.</p>
                 <p>Subscription: {payment.Subscription.Type.Name}</p>
                 <p>Payment Date: {payment.PaidDate}</p>";


            await SendEmailAsync(email, subject, body);
        }

        public async Task SendPaymentFailedEmail(string email, Payment payment)
        {

            var subject = $"Payment Failed for {payment.Subscription.Type.Name} Subscription";
            var body = $@"
            <h1>Payment Failed</h1>
            <p>We were unable to process your payment of {payment.Amount} {payment.Currency}.</p>
            <p>Please update your payment method to avoid service interruption.</p>";

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendSubscriptionCancelledEmail(string email, Subscription subscription)
        {
            var subject = $"Your {subscription.Type.Name} Subscription Has Been Cancelled";
            var body = $@"
            <h1>Subscription Cancelled</h1>
            <p>Your {subscription.Type.Name} subscription has been cancelled.</p>
            <p>We're sorry to see you go. You can reactivate your subscription anytime.</p>";

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendSubscriptionExpiredEmail(string email, string subscriptionType)
        {
             var subject = $"Your {subscriptionType} Subscription Has Expired";
             var body = $@"
            <h1>Subscription Expired</h1>
            <p>Your {subscriptionType} subscription has expired.</p>
            <p>Renew now to continue using our services.</p>";
        
        await SendEmailAsync(email, subject, body);
        }

        public async Task SendSubscriptionExpiringSoonEmail(string email, string subscriptionType, DateTime endDate)
        {
            var subject = $"Your {subscriptionType} Subscription Is Expiring Soon";
            var body = $@"
            <h1>Subscription Expiring Soon</h1>
            <p>Your {subscriptionType} subscription will expire on {endDate:MM/dd/yyyy}.</p>
            <p>Renew now to avoid interruption of service.</p>";

            await SendEmailAsync(email, subject, body);
        }

        public async Task NotifyTrainerForSession(string email, Session session)
        {
            var subject = "New Training Session Assigned to You";

            var body = $@"
                        <html>
                        <body style='font-family: Arial, sans-serif;'>
                            <h2>📅 Session Details</h2>
                            <p><strong>Session ID:</strong> {session.Id}</p>
                            <p><strong>Room ID:</strong> {session.RoomId}</p>
                            <p><strong>Date:</strong> {session.Date:yyyy-MM-dd}</p>
                            <p><strong>Start Time:</strong> {session.StartTime}</p>
                            <p><strong>End Time:</strong> {session.EndTime}</p>
                            <p><strong>Session Type:</strong> {session.Type?.Name ?? "N/A"}</p>

                            <p style='margin-top:20px;'>If you didn’t request this, you can safely ignore this email.</p>
                            
                            <p style='color:#000000; font-size: 16px; margin-top: 20px;'>Best regards,</p>
                            <p style='color: #000; font-style: italic; font-weight: bold; font-size: 18px;'>Gymawy Team</p>
                        </body>
                        </html>";

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendBookingConfirmationEmail(string email, Booking booking)
        {
            var subject = $"Booking Confirmation for {booking.Id} ";
            var body = $@"
                 <h1>Thank you for your payment!</h1>
                 <p>We've successfully processed your payment of {booking.AmountPaid}</p>
                 <p>Booking Date {booking.BookingDate}</p>";
                

            await SendEmailAsync(email, subject, body);
        }
    }
}
