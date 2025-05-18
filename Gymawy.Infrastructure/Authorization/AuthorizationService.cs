using Gymawy.Domain.Users;
using Gymawy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Authorization
{
    public  class AuthorizationService
    {
        private readonly ApplicationDbContext _context;

        public AuthorizationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserRoleResponse> GetRoleForUserAsync (Guid userId)
        {
            var user = await _context.Set<User>().FindAsync(userId);

            if (user is null)
            {
              return new UserRoleResponse 
              {
                 UserId = Guid.Empty,
                 RoleName = string.Empty
              
              };
            }


            var role = user.GetProfileType().Name;

            return new UserRoleResponse
            {

                UserId = userId,
                RoleName = role
            };
        }
    }
}
