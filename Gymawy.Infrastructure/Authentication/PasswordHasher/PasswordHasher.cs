using BCrypt.Net;
using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Authentication.PasswordHasher
{
    public class PasswordHasher : IPasswordHasher
    {
        public ErrorOr<string> HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);

            
        }

        public bool IsCorrectPassword(string password, string hash)
        {
           
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
        }
    }
}
