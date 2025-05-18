using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.TrainerInvitations;
using Gymawy.Domain.Trainers;
using Gymawy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Trainers.Commands.RespondToInvitation
{
    public class RespondToInvitationCommandHandler : IRequestHandler<RespondToInvitationCommand, ErrorOr<TrainerInvitaion>>
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsersRepository _usersRepository;
        private readonly IUserContext _userContext;
        private readonly ITrainersRepository _trainersRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IGymsRepository _gymsRepository;
        private readonly ITrainerInvitationsRepository  _trainerInvitationsRepository;



        public RespondToInvitationCommandHandler(
            IUnitOfWork unitOfWork,
            IUsersRepository usersRepository,
            IUserContext userContext,
            ITrainersRepository rainersRepository,
            IDateTimeProvider dateTimeProvider,
            IGymsRepository gymsRepository,
            ITrainerInvitationsRepository trainerInvitationsRepository)
        {
            _unitOfWork = unitOfWork;
            _usersRepository = usersRepository;
            _userContext = userContext;
            _trainersRepository = rainersRepository;
            _dateTimeProvider = dateTimeProvider;
            _gymsRepository = gymsRepository;
            _trainerInvitationsRepository = trainerInvitationsRepository;
        }

        public async Task<ErrorOr<TrainerInvitaion>> Handle(RespondToInvitationCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;

            var trainer = await _trainersRepository.FindAsync(x => x.UserId == userId , new[] {nameof(Trainer.TrainerInvitaions)} , cancellationToken);

            if (!trainer.HasInvitation(request.InvitaionId) )
                return Error.Conflict(description: "trainer invitation has no id like this");


            var invitation = await _trainerInvitationsRepository.FindAsync(x => x.Id == request.InvitaionId, new[] { nameof(TrainerInvitaion.Gym) }, cancellationToken);

            if (invitation.Status != InvitationStatus.Pending)
                return TrainerInvitationErrors.InvalidState;

          

            if (request.Responde == InvitationRespond.Accept)

            {
               if (invitation.Gym.HasTrainer(trainer.Id))
                    return GymErrors.TrainerAlreadyAssigned;

                invitation.Accept(_dateTimeProvider.UtcNow);

                var result = invitation.Gym.AssignTrainer(trainer);

                if (result.IsError)
                    return result.Errors;
                await _unitOfWork.CompleteAsync();
             
            }
            else
            {
               var result =  invitation.Reject(_dateTimeProvider.UtcNow);

                if (result.IsError)
                    return result.Errors;
                await _unitOfWork.CompleteAsync();

            }

            return invitation;
        }
    }
}
