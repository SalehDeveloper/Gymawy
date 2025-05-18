using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.GymTrainers;
using Gymawy.Domain.TrainerInvitations;
using Gymawy.Domain.Trainers;
using Gymawy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Gyms.Commands.InviteTrainer
{
    public class InviteTrainerCommandHandler : IRequestHandler<InviteTrainerCommand, ErrorOr<TrainerInvitaion>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGymsRepository _gymsRepository;
        private readonly ITrainersRepository _trainerRepository;
        private readonly IUserContext _userContext;
        private readonly IAdminsRepository _adminsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ITrainerInvitationsRepository _trainerInvitationsRepository;
        private readonly IDateTimeProvider _dateTimeProvider;



        public InviteTrainerCommandHandler(
            IUnitOfWork unitOfWork,
            IGymsRepository gymsRepository,
            ITrainersRepository trainerRepository,
            IUserContext userContext,
            IAdminsRepository adminsRepository,
            IUsersRepository usersRepository,
            ITrainerInvitationsRepository trainerInvitationsRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _gymsRepository = gymsRepository;
            _trainerRepository = trainerRepository;
            _userContext = userContext;
            _adminsRepository = adminsRepository;
            _usersRepository = usersRepository;
            _trainerInvitationsRepository = trainerInvitationsRepository;
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task<ErrorOr<TrainerInvitaion>> Handle(InviteTrainerCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;

            var admin = await _adminsRepository.FindAsync(x => x.UserId == userId);

            var gym = await _gymsRepository.FindAsync(
                x => x.Id == request.GymId,
                new[] { nameof(Gym.Subscription), nameof(Gym.Trainers), nameof(Gym.GymTrainers) }
                , cancellationToken);

            if (gym is null || gym.Subscription.AdminId != admin.Id)
                return Error.Validation(description: "gym not found or you cannot access this gym");

            var trainer = await _trainerRepository.FindAsync(
                x => x.Id == request.TrainerId,
                new[] { nameof(Trainer.TrainerInvitaions) }, 
                cancellationToken);

            if (trainer == null)
                return Error.Validation(description: "trainer not found");


           var result =  gym.InviteTrainer(trainer , _dateTimeProvider.UtcNow);
          
            if (result.IsError)
                return result.Errors;
        
            await _unitOfWork.CompleteAsync();

            return result;

        }
    }
}
