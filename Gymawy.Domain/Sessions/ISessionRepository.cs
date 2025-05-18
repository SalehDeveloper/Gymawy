using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Sessions
{
    public interface ISessionRepository :IBaseRepository<Session>
    {
        public Task<Admin> GetAdminIdForSession(Guid sessionId);
    }
}
