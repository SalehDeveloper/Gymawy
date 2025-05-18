using ErrorOr;
using Gymawy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Profiles.Queries.GetProfileByEmail
{
    public record GetProfileByEmailQuery(string Email)
        :IRequest<ErrorOr<User>>;
    
    
}
