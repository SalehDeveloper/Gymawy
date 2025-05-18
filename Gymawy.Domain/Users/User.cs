using ErrorOr;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using Gymawy.Domain.ProfileTypes;
using Gymawy.Domain.RefreshTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Users
{
    public class User : BaseEntity
    {
        public static int EmailCodeExpiresAfterMinutes = 30;
        public string FullName { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string PhotoUrl { get; private set; }

        public bool EmailConfirmed { get; private set; }
        public string? EmailConfirmationCode { get; private set; }
        public DateTime? EmailConfirmationCodeExpiresAt { get; private set; }

        public bool IsActive { get; private set; }  
        public Guid? AdminId { get; private set; }
        public Guid? TrainerId { get; private set; }
        public Guid? ParticipantId { get; private set; }

        public ICollection<RefreshToken> RefreshTokens { get; private set; }


        public User
            (
             string fullName,
             string email,
             string password,
             DateTime createdOnUtc,
             string photoUrl,
             Guid? adminId = null,
             Guid? trainerId = null,
             Guid? participantId = null,
            Guid? id = null
            ) : base(createdOnUtc, id)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            PhotoUrl = photoUrl;
            AdminId = adminId;
            TrainerId = trainerId;
            ParticipantId = participantId;
            EmailConfirmed = false;
            EmailConfirmationCode = null;
            EmailConfirmationCodeExpiresAt = null;
            RefreshTokens = new List<RefreshToken>();
            IsActive = false;

        }
        protected User()
        {

        }
        public ErrorOr<Success> ConfirmEmail(string code)
        {
            if (EmailConfirmed)
                return UserErrors.EmailAlreadyConfirmed;

            if (EmailConfirmationCodeExpiresAt.Value < DateTime.UtcNow || code != EmailConfirmationCode)
            {
                return UserErrors.InvalidOrExpiredEmailCode;

            }

            EmailConfirmed = true;
            EmailConfirmationCode = null;
            EmailConfirmationCodeExpiresAt = null;
            IsActive = true;
            return Result.Success;

        }
        public void SetPhotoUrl(string photoUrl)
        {
            PhotoUrl = photoUrl;
        }
        public void SetEmailConfirmationCode(string emailConfirmationCode, DateTime expiredAtUtc)
        {
            EmailConfirmationCode = emailConfirmationCode;
            EmailConfirmationCodeExpiresAt = expiredAtUtc.AddMinutes(EmailCodeExpiresAfterMinutes);
        }
        public ErrorOr<Success> Activate()
        {
            if (this.IsActive)
                return UserErrors.Active;

            IsActive = true;
            return Result.Success;
        }
        public ErrorOr<Success> DeActivate()
        {
            if (!this.IsActive)
                return UserErrors.Inactive;

            IsActive = false;

            return Result.Success;
        }
        public ProfileType GetProfileType()
        {
            return AdminId is not null ? ProfileType.Admin :
                           TrainerId is not null ? ProfileType.Trainer :
                           ProfileType.Participant;
        }
        public ErrorOr<Guid> CreateAdminProfile()
        {

            if (DoesUserHasAProfile())
                return UserErrors.AlreadyHasAProfile;

            AdminId = Guid.NewGuid();
            return AdminId.Value;
        }

        public ErrorOr<Guid> CreateParticipantProfile()
        {
            if (DoesUserHasAProfile())
                return UserErrors.AlreadyHasAProfile;

            ParticipantId = Guid.NewGuid();
            return ParticipantId.Value;
        }

        public ErrorOr<Guid> CreateTrainerProfile()
        {
            if (DoesUserHasAProfile())
                return UserErrors.AlreadyHasAProfile;

            TrainerId = Guid.NewGuid();
            return TrainerId.Value;
        }

        private bool DoesUserHasAProfile()
        {
            if (
                AdminId is not null
                ||
                TrainerId is not null
                ||
                ParticipantId is not null
                )

                return true;    

            return false;
        }

    }
}
