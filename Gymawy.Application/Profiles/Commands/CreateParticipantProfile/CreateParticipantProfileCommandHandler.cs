using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Participants;
using Gymawy.Domain.Users;
using MediatR;


namespace Gymawy.Application.Profiles.Commands.CreateParticipantProfile
{
    public class CreateParticipantProfileCommandHandler : IRequestHandler<CreateParticipantCommand, ErrorOr<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUsersRepository _usersRepository;
        private readonly IParticipantsRepository _participantsRepository;

        public CreateParticipantProfileCommandHandler(
            IUnitOfWork unitOfWork,
            IUserContext userContext,
            IDateTimeProvider dateTimeProvider,
            IUsersRepository usersRepository,
            IParticipantsRepository participantsRepository)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _dateTimeProvider = dateTimeProvider;
            _usersRepository = usersRepository;
            _participantsRepository = participantsRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateParticipantCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;

            var user = await _usersRepository.FindAsync(x => x.Id == userId, null, cancellationToken);

            if (user == null)
                return UserErrors.NotFound;


            var participantId = user.CreateParticipantProfile();
            if (participantId.IsError)
                return participantId.Errors;

            var participant = new Participant( userId , _dateTimeProvider.UtcNow , participantId.Value);

            var result = await _participantsRepository.AddAsync(participant, cancellationToken);

            await _unitOfWork.CompleteAsync();

            return participant.Id;



        }
    }
}
