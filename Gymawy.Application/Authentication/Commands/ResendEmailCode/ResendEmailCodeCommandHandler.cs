using ErrorOr;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Application.Abstractions.Email;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Authentication.Commands.ResendEmailCode
{
    public class ResendEmailCodeCommandHandler : IRequestHandler<ResendEmailCodeCommand, ErrorOr<string>>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;


        public ResendEmailCodeCommandHandler(IUsersRepository usersRepository, IDateTimeProvider dateTimeProvider, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _usersRepository = usersRepository;
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<ErrorOr<string>> Handle(ResendEmailCodeCommand request, CancellationToken cancellationToken)
        {
            var user  = await _usersRepository.FindAsync(x=> x.Email == request.Email , null , cancellationToken) ;

            if (user == null) 
                return UserErrors.NotFound;

            if (user.EmailConfirmed)
                return UserErrors.EmailAlreadyConfirmed;

            var code = CodeGenerator.Generate8DigitCode();

            await _emailService.SendConfirmationToEamilAsync(user.Email, "Email confiramtion code", "confirm your email", code, User.EmailCodeExpiresAfterMinutes.ToString());
           
            user.SetEmailConfirmationCode(code, _dateTimeProvider.UtcNow);

           await _unitOfWork.CompleteAsync();

            return $"code sent successfully , check your email";

        }
    }
}
