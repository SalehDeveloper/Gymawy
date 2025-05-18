using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Application.Abstractions.Email;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.Rooms;
using Gymawy.Domain.Sessions;
using Gymawy.Domain.Subscriptions;
using Gymawy.Domain.Trainers;
using Gymawy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Sessions.Commands.CreateSession
{
    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, ErrorOr<Session>>
    {   
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsersRepository _usersRepository;
        private readonly IAdminsRepository _adminsRepository;
        private readonly IUserContext _userContext; 
        private readonly IGymsRepository _gymsRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IRoomsRepository _roomsRepository;
        private readonly ITrainersRepository _trainersRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ISessionRepository _sessionRepository;
        private readonly IEmailService _emailService;

        public CreateSessionCommandHandler(
            IUnitOfWork unitOfWork,
            IUsersRepository usersRepository,
            IAdminsRepository adminsRepository,
            IUserContext userContext,
            IGymsRepository gymsRepository,
            ISubscriptionsRepository subscriptionsRepository,
            IRoomsRepository roomsRepository,
            ITrainersRepository trainersRepository,
            IDateTimeProvider dateTimeProvider,
            ISessionRepository sessionRepository,
            IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _usersRepository = usersRepository;
            _adminsRepository = adminsRepository;
            _userContext = userContext;
            _gymsRepository = gymsRepository;
            _subscriptionsRepository = subscriptionsRepository;
            _roomsRepository = roomsRepository;
            _trainersRepository = trainersRepository;
            _dateTimeProvider = dateTimeProvider;
            _sessionRepository = sessionRepository;
            _emailService = emailService;
        }

        public async Task<ErrorOr<Session>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
             
            var admin = await _adminsRepository.FindAsync(x => x.UserId == userId  , new[] {nameof(Admin.User)});

            var subscription = await _subscriptionsRepository.FindAsync(x => x.Id == admin.SubscriptionId, new[] { nameof(Subscription.Gyms) }, cancellationToken);

            var room = await _roomsRepository.FindAsync( x=>x.Id ==  request.RoomId , new[] {nameof(Room.Gym)});
            
            var gym = room.Gym;

            var adminHasAccessToRoom = subscription.HasGym(gym.Id);

            if (!adminHasAccessToRoom)
                return Error.Validation(description: "Room not found or access denied.");

            var trainer = await _trainersRepository.FindAsync(x=> x.Id== request.TrainerId , new[] {nameof(Trainer.Sessions),nameof(Trainer.Gyms)} , cancellationToken);

            if (trainer is null)
                return TrainerErrors.NotFound;

            var hasTrainer = gym.HasTrainer(trainer.Id);
            if (!hasTrainer)
                return Error.NotFound(description: "Trainer not found or is unavailable for this gym.");

            var session = new Session(
                request.RoomId ,
                request.TrainerId,
                request.Type ,
                request.Description,
                request.MaxParticipants,
                request.Date,
                request.StartTime,
                request.EndTime,
                request.SessionFee,
                _dateTimeProvider.UtcNow);

            if (!trainer.IsAvailableForSession(session))
                return TrainerErrors.OverlappedSession;

            var result = room.AddSession(session);

            if (result.IsError)
                return result.Errors;

            await _unitOfWork.CompleteAsync();
            await _emailService.NotifyTrainerForSession(admin.User.Email, session);
            return session;







        }
    }
}
