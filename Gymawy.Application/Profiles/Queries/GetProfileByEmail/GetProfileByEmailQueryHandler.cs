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
    public class GetProfileByEmailQueryHandler : IRequestHandler<GetProfileByEmailQuery, ErrorOr<User>>
    {
        private readonly IUsersRepository _usersRepository;

        public GetProfileByEmailQueryHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<ErrorOr<User>> Handle(GetProfileByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.FindAsync(x=> x.Email  == request.Email, null , cancellationToken);
            
            if (user == null) 
                return UserErrors.NotFound; 
            return user;
        }
    }
}
