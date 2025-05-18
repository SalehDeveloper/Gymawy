using Gymawy.Application.Abstractions.Auth;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Authentication
{
    public class UserContext : IUserContext
    { 
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContext(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid UserId =>
            _contextAccessor.HttpContext?
            .User
            .GetUserId()??
            throw new ApplicationException("User context is unavailable");
    }
}
