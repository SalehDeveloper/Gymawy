using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Users
{
    public static class UserErrors
    {
        public static Error InvalidOrExpiredEmailCode = Error.Validation(
        code: "User.InvalidEmailCode",
        description: "Invalid or expired confirmation code, please request a new one.");

        public static Error Active = Error.Conflict(
            code: "User.AlreadyActive",
            description: "User is active.");

        public static Error Inactive = Error.Conflict(
            code: "User.Inactive",
            description: "User is inactive.");

        public static Error EmailAlreadyConfirmed = Error.Conflict(
           code: "User.EmailAlreadyConfirmed",
           description: "User email already confirmed.");


        public static Error NotFound = Error.NotFound(
        code: "User.NotFound",
        description: "User not found.");


        public static Error AlreadyHasAProfile = Error.Conflict(
        code: "User.AlreadyHasAProfile",
        description: "User already has a profile.");


        public static Error AlreadyExists = Error.Conflict(
        code: "User.EmailAddressAlradyExists",
        description: "User with this email already exist.");



    }
}
