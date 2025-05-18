using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Abstractions.Auth
{
    public interface IPasswordHasher
    {
        public ErrorOr<string> HashPassword(string password);
        bool IsCorrectPassword(string password, string hash);
    }
}
