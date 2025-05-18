using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.RefreshTokens
{
    public class RefreshToken : BaseEntity
    {

        public string Token { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsActive {  get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public RefreshToken
            (string token , 
            DateTime expiresAt , 
            Guid userId,
            DateTime createdOnUtc)
            : base(createdOnUtc)
        {
            Token = token;
            ExpiresAt = expiresAt;
            IsActive = true;
            UserId = userId;
            CreatedOnUtc = createdOnUtc;
        }

        protected RefreshToken()
        {

        }
        public void Revoke()
        {
            IsActive = false;
        }
    }
}
