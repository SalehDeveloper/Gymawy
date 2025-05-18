using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Authorization
{
    public class UserRoleResponse
    {
        public Guid UserId { get; init; }

        public string RoleName { get; init; } 
    }
}
