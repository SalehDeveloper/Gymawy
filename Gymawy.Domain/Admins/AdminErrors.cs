using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Admins
{
    public static class AdminErrors
    {
        public static readonly Error AlreadyHasSubscription = 
            Error.Conflict(
                code: "Admin.AlreadyHasSubscription",
                description: "admin already has a subscription");

        public static readonly Error AlreadyHasStripeAccount =
            Error.Conflict(
                code: "Admin.AlreadyHasStripeAccount",
                description: "admin already has a stripe account");


    }
}
