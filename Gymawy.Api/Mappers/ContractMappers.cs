using Gymawy.Application.Authentication.Commands.Login.Common;
using Gymawy.Application.Bookings.Commands.VerifyBooking;
using Gymawy.Application.Participants.Commands.SpotSession;
using Gymawy.Application.Subscriptions.Commands.CreateSubscription;
using Gymawy.Contract.Authentication;
using Gymawy.Contract.Bookings;
using Gymawy.Contract.Gyms;
using Gymawy.Contract.Profiles;
using Gymawy.Contract.Rooms;
using Gymawy.Contract.Sessions;
using Gymawy.Contract.Subscriptions;
using Gymawy.Domain.Bookings;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.Rooms;
using Gymawy.Domain.Subscriptions;
using Gymawy.Domain.TrainerInvitations;
using Gymawy.Domain.Users;
using Org.BouncyCastle.Tls.Crypto;
using System.Runtime.CompilerServices;
using static Gymawy.Api.ApiEndpoints;

namespace Gymawy.Api.Mappers
{
    public static class ContractMappers 
    {
        public static LoginResponse MapToLoginResponse( this LoginResult result)
        {
            return new LoginResponse
                (
                result.UserId,
                result.Email,
                result.FullName,
                result.PhotoUrl,
                result.ProfileType);
        }

        public static SubscriptionResponse MapToSubscriptionResponse (this SubscriptionResult subscription)
        {
            return new SubscriptionResponse(
                subscription.Subscription.Id , 
                subscription.Subscription.AdminId ,
                subscription.Subscription.Type.ToDto() ,
                subscription.Subscription.Status.ToDto(),
                subscription.sessionUrl, 
                "Complete your payment to activate the subscription, unless you're on the free plan.");
        }

        public static Contract.Subscriptions.SubscriptionType ToDto (this Domain.Subscriptions.SubscriptionType subscription)
        {
            return subscription.Name switch
            {
                nameof(Domain.Subscriptions.SubscriptionType.Free) => Contract.Subscriptions.SubscriptionType.Free,
                nameof(Domain.Subscriptions.SubscriptionType.Starter) => Contract.Subscriptions.SubscriptionType.Starter,
                nameof(Domain.Subscriptions.SubscriptionType.Pro) => Contract.Subscriptions.SubscriptionType.Pro,
                _ => throw new InvalidOperationException()

            };
        }

        public static Contract.Subscriptions.SubscriptionStatus ToDto(this Domain.Subscriptions.SubscriptionStatus subscription)
        {
            return subscription.Name switch
            {
                nameof(Domain.Subscriptions.SubscriptionStatus.Pending) => Contract.Subscriptions.SubscriptionStatus.Pending,
                nameof(Domain.Subscriptions.SubscriptionStatus.Active) => Contract.Subscriptions.SubscriptionStatus.Active,
                nameof(Domain.Subscriptions.SubscriptionStatus.PastDue) => Contract.Subscriptions.SubscriptionStatus.PastDue,
                nameof(Domain.Subscriptions.SubscriptionStatus.Canceled) => Contract.Subscriptions.SubscriptionStatus.Canceled,
                nameof(Domain.Subscriptions.SubscriptionStatus.Expired) => Contract.Subscriptions.SubscriptionStatus.Expired,
                _ => throw new InvalidOperationException()

            };
        }
    
        public static GymResponse MapToGymResponse(this Domain.Gyms.Gym gym)
        {
            if (gym.MaxRooms == int.MaxValue)
            return new GymResponse(gym.Id, gym.Name, "Unlimited");

            return new GymResponse(gym.Id, gym.Name, gym.MaxRooms.ToString());
        }

        public static Contract.Profiles.ProfileType ToDto(this Domain.ProfileTypes.ProfileType profile)
        {
            return profile.Name switch
            {
                nameof(Domain.ProfileTypes.ProfileType.Participant) => Contract.Profiles.ProfileType.Participant,
                nameof(Domain.ProfileTypes.ProfileType.Trainer) => Contract.Profiles.ProfileType.Trainer,
                nameof(Domain.ProfileTypes.ProfileType.Admin) => Contract.Profiles.ProfileType.Admin,

                _ => throw new InvalidOperationException()

            };

        }

        public static ProfileResponse MapToProfileResponse (this User user)
        {
            
            return new ProfileResponse
                (
                user.Id,
                user.FullName,
                user.Email,
                user.GetProfileType().ToDto() ,
                user.IsActive
                );
        }
    
        public static RoomResponse MapToResponse( this Domain.Rooms.Room room)
        {
            if (room.MaxDailySessions == int.MaxValue)
                return new RoomResponse(room.Id, room.GymId, room.Name, "Unlimited");

            return new RoomResponse(room.Id, room.GymId, room.Name, room.MaxDailySessions.ToString()) ;


        }

        public static InvitationResponse MapToResponse(this TrainerInvitaion invitaion , string message)
        {
            return new InvitationResponse(invitaion.Id, invitaion.GymId, invitaion.TrainerId, invitaion.Status.ToDto(), message);
        }

        public static Contract.Gyms.InvitationRespond ToDto (this InvitationStatus status)
        {
            return status.Name switch
            {
                nameof(Domain.TrainerInvitations.InvitationStatus.Accepted) => Contract.Gyms.InvitationRespond.Accepted,
                nameof(Domain.TrainerInvitations.InvitationStatus.Rejected) => Contract.Gyms.InvitationRespond.Rejected,
                nameof(Domain.TrainerInvitations.InvitationStatus.Pending) => Contract.Gyms.InvitationRespond.Pending,


                _ => Contract.Gyms.InvitationRespond.Pending

            };

        }

        public static Contract.Sessions.SessionType MapToContract(this Domain.Sessions.SessionType  type)
        {
            return type.Name switch
            {
                nameof(Domain.Sessions.SessionType.Functional) => Contract.Sessions.SessionType.Functional,
                nameof(Domain.Sessions.SessionType.Zoomba) => Contract.Sessions.SessionType.Zoomba,
                nameof(Domain.Sessions.SessionType.Yoga) => Contract.Sessions.SessionType.Yoga,
                nameof(Domain.Sessions.SessionType.Kickboxing) => Contract.Sessions.SessionType.Kickboxing,
                nameof(Domain.Sessions.SessionType.Pilates) => Contract.Sessions.SessionType.Pilates,




            };
        }

        public static Contract.Sessions.SessionStatus MapToContract(this Domain.Sessions.SessionStatus status)
        {
            return status.Name switch
            {
                nameof(Domain.Sessions.SessionStatus.CommingSoon) => Contract.Sessions.SessionStatus.CommingSoon,
                nameof(Domain.Sessions.SessionStatus.Completed) => Contract.Sessions.SessionStatus.Completed,
                nameof(Domain.Sessions.SessionStatus.Cancelled) => Contract.Sessions.SessionStatus.Cancelled,

            };
        }

        public static SessionResponse MapToResponse(this Domain.Sessions.Session session)
        {
            return new SessionResponse(
                session.Id,
                session.RoomId,
                session.TrainerId,
                session.Type.MapToContract(),
                session.Description,
                session.Date,
                session.StartTime,
                session.EndTime,
                session.SessionFee,
                session.Status.MapToContract());
        }

        public static SpotSessionResponse MapToResponse(this SpotSessionResult result)
        {
            return new SpotSessionResponse
                ( 
                    result.Booking.Id , 
                    result.Booking.SessionId , 
                    result.Booking.ParticipantId ,
                    result.Booking.Status.MapToContract(),
                    result.Booking.BookingDate , 
                    result.SessionUrl



                );
        }

        public static Contract.Bookings.BookingStatus MapToContract( this Domain.Bookings.BookingStatus status )
        {
            return status.Name switch
            {
                nameof(Domain.Bookings.BookingStatus.Pending) => Contract.Bookings.BookingStatus.Pending,
                nameof(Domain.Bookings.BookingStatus.Failed) => Contract.Bookings.BookingStatus.Failed,
                nameof(Domain.Bookings.BookingStatus.Refunded) => Contract.Bookings.BookingStatus.Refunded,
                nameof(Domain.Bookings.BookingStatus.Confirmed) => Contract.Bookings.BookingStatus.Confirmed


            };
        }

        public static BookingResponse MapToResponse (this BookingResult result)
        {
            return new BookingResponse
                (
                  result.Booking.Id  , 
                  result.Booking.SessionId , 
                  result.Booking.ParticipantId ,
                  result.Booking.BookingDate , 
                  result.Booking.AmountPaid , 
                  result.Booking.Status.MapToContract(),
                  result.message



                );
        }
       
    }
}
