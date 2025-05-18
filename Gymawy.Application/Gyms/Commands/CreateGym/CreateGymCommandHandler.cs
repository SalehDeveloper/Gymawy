using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.Subscriptions;
using Gymawy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Gyms.Commands.CreateGym
{
    public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IUsersRepository _usersRepository;
        private readonly IAdminsRepository _adminsRepository;
        private readonly IDateTimeProvider _dateTimeProvider;


        public CreateGymCommandHandler(
            ISubscriptionsRepository subscriptionsRepository,
            IUnitOfWork unitOfWork,
            IUserContext userContext,
            IUsersRepository usersRepository,
            IAdminsRepository adminsRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _usersRepository = usersRepository;
            _adminsRepository = adminsRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<Gym>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
        {
           var userId = _userContext.UserId;

           var admin= await _adminsRepository.FindAsync(x=> x.UserId == userId , new[] { "Subscription" } , cancellationToken);

           var subscription = await _subscriptionsRepository.FindAsync(x=> x.AdminId ==  admin.Id , new[] { "Gyms" } , cancellationToken);

            var gym = new Gym(subscription.Id, request.Name, subscription.GetMaxRooms(), _dateTimeProvider.UtcNow);

            var result = subscription.AddGym(gym);
          
            if (result.IsError)
                return result.Errors;

           await _unitOfWork.CompleteAsync();

            return gym;
        }
    }
}
