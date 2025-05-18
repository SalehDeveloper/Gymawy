using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Participants;
using Gymawy.Domain.Trainers;
using Gymawy.Domain.Users;
using MediatR;

namespace Gymawy.Application.Profiles.Commands.CreateTrainerProfile
{
    public class CreateTrainerProfileCommandHandler : IRequestHandler<CreateTrainerProfileCommand, ErrorOr<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUsersRepository _usersRepository;
        private readonly ITrainersRepository _trainingersRepository;

        public CreateTrainerProfileCommandHandler(
            IUnitOfWork unitOfWork,
            IUserContext userContext,
            IDateTimeProvider dateTimeProvider,
            IUsersRepository usersRepository,
            ITrainersRepository trainingersRepository)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _dateTimeProvider = dateTimeProvider;
            _usersRepository = usersRepository;
            _trainingersRepository = trainingersRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateTrainerProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;

            var user = await _usersRepository.FindAsync(x => x.Id == userId, null, cancellationToken);

            if (user == null)
                return UserErrors.NotFound;


            var trainerId = user.CreateTrainerProfile();
            if (trainerId.IsError)
                return trainerId.Errors;

            var participant = new Trainer( userId, request.Specialty , request.Certification , request.Bio ,_dateTimeProvider.UtcNow , trainerId.Value);

            var result = await _trainingersRepository.AddAsync(participant, cancellationToken);

            await _unitOfWork.CompleteAsync();

            return participant.Id;
        }
    }
}
