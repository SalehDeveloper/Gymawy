using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Application.Abstractions.Stripe;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using Gymawy.Domain.Bookings;
using Gymawy.Domain.Participants;
using Gymawy.Domain.Sessions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Participants.Commands.SpotSession
{
    public class SpotSessionCommandHandler : IRequestHandler<SpotSessionCommand, ErrorOr<SpotSessionResult>>
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStripeService _stripeService;
        private readonly IUserContext _userContext;
        private readonly ISessionRepository _sessionRepository;
        private readonly IParticipantsRepository _participantsRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public SpotSessionCommandHandler(
            IUnitOfWork unitOfWork,
            IStripeService stripeService,
            IUserContext userContext,
            ISessionRepository sessionRepository,
            IParticipantsRepository participantsRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _stripeService = stripeService;
            _userContext = userContext;
            _sessionRepository = sessionRepository;
            _participantsRepository = participantsRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<SpotSessionResult>> Handle(SpotSessionCommand request, CancellationToken cancellationToken)
        {
            var userId= _userContext.UserId;
          
            
            
            var participnt = await _participantsRepository.FindAsync(x=> x.UserId == userId , new[] {nameof(Participant.Bookings)});


            var session  = await _sessionRepository.GetByIdAsync(request.SessionId);

          

            if (session == null) 
                return SessionErrors.NotFound;

            if (session.IsFull())
                return SessionErrors.Full;

          var booking =  participnt.SpotSession(session , _dateTimeProvider.UtcNow);

            if (booking.IsError)
                return booking.Errors;

            await _unitOfWork.CompleteAsync();

            
            var admin =await _sessionRepository.GetAdminIdForSession(request.SessionId);
            
            
            var sessionUrl = await _stripeService.CreateCheckOutSessionForSpotAsync(booking.Value.Id, session.Id, session.SessionFee, admin.StripeAccountId);


            

            return new SpotSessionResult(booking.Value, sessionUrl);


        }
    }
}
