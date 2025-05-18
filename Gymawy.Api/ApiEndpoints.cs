namespace Gymawy.Api
{
    public static class ApiEndpoints
    {
        private const string apiBase = "api";
        public static class Authenation
        {
            public const string Base = $"{apiBase}/authentication";

            public const string Regiser = $"{Base}/register";

            public const string ConfirmEmail = $"{Base}/confirm-email";

            public const string ResendEmailCode = $"{Base}/resend-emailCode";

            public const string Login = $"{Base}/login";

            public const string RefreshToken = $"{Base}/refresh-token";
        }
        public static class Profile
        {
            public const string Base = $"{apiBase}/profiles";
            public const string CreateAdminProfile = $"{apiBase}/profiles/admin";
            public const string CreatePartitipantProfile = $"{apiBase}/profiles/participant";
            public const string CreateTrainerProfile = $"{apiBase}/profiles/trainer";
            public const string CreateStripeAccount = $"{apiBase}/profiles/stripe";
            public const string GetByEmail = $"{Base}/{{email}}";


        }

        public static class Subscription
        {

            public  const  string Base = $"{apiBase}/subscriptions";

            public  const string CreateSubscription = $"{Base}";
        }
        public static class Payments
        {
            public const string Base = $"{apiBase}/payments";
            public const string Retry = $"{apiBase}/payments/retry";
            public const string Verify = $"{apiBase}/payments/{{sessionId}}/verify";


        }
        public static class Stripe
        {
            public const string Base = $"{apiBase}/webhooks/stripe";
        }
        public static class Gym
        {
            public const string Base = $"{apiBase}/gyms";
            public const string Create = $"{Base}";
            public const string AddRoom = $"{Base}/{{gymId:guid}}/rooms";
            public const string InviteTrainer = $"{Base}/{{gymId:guid}}/trainers/{{trainerId:guid}}";

        }
        public static class Room
        {
            public const string Base = $"{apiBase}/rooms";
            public const string AddSession = $"{Base}/{{roomId:guid}}/sessions";
        } 

        public static class Trainer
        {
            public const string Base = $"{apiBase}/trainers";
        }

        public static class Invitation
        {
            public const string Base = $"{apiBase}/invitations";
            public const string Respond = $"{apiBase}/invitations/{{invitationId:guid}}/respond";
        }

        public static class Session
        {
            public const string Base = $"{apiBase}/sessions";

            public const string Spot = $"{apiBase}/sessions/{{sessionId:guid}}/spot";


        }

        public static class Booking
        {
            public const string Base = $"{apiBase}/bookings";
            public const string Verify = $"{apiBase}/bookings/{{sessionId}}/verify";
        }

    }
}
