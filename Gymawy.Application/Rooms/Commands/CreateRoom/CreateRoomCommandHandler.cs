using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.Rooms;
using Gymawy.Domain.Subscriptions;
using Gymawy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRoomsRepository _roomsRepository;
        private readonly IAdminsRepository _adminsRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IGymsRepository _gymsRepository;
        private readonly IDateTimeProvider _dateTimeProvider;



        public CreateRoomCommandHandler(
            IUsersRepository usersRepository,
            IRoomsRepository roomsRepository,
            IAdminsRepository adminsRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork,
            ISubscriptionsRepository subscriptionsRepository,
            IGymsRepository gymsRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _usersRepository = usersRepository;
            _roomsRepository = roomsRepository;
            _adminsRepository = adminsRepository;
            _userContext = userContext;
            _unitOfWork = unitOfWork;
            _subscriptionsRepository = subscriptionsRepository;
            _gymsRepository = gymsRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;

            var admin = await _adminsRepository.FindAsync(x => x.UserId == userId);

            var gym = await _gymsRepository.FindAsync(x => x.Id == request.GymId, new[] { "Rooms", "Subscription" }, cancellationToken);

            if (gym is null || gym.Subscription.AdminId != admin.Id)
                return Error.Validation(description: "gym not found or you cannot access this gym");

            var room = new Room(request.Name, gym.Subscription.GetMaxDailySessions(), gym.Id, _dateTimeProvider.UtcNow);

           var result =  gym.AddRoom(room);

            if (result.IsError)
                return result.Errors;
            await _unitOfWork.CompleteAsync();

            return room; 
           
        }
    }
}
