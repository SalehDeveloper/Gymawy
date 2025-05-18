
using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Application.Abstractions.Email;
using Gymawy.Application.Abstractions.Storage;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Users;
using MediatR;

namespace Gymawy.Application.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ICloudinaryService _fileService;
        private readonly IUsersRepository _usersRepository;
        private readonly IEmailService _emailService;
    

        public RegisterUserCommandHandler(
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IDateTimeProvider dateTimeProvider,
            ICloudinaryService fileService,
            IEmailService emailService,
            IUsersRepository usersRepository)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _dateTimeProvider = dateTimeProvider;
            _fileService = fileService;
            _emailService = emailService;
            _usersRepository = usersRepository;
        }

        public async Task<ErrorOr<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _usersRepository.FindAsync(x => x.Email == request.Email, null, cancellationToken);

            if (userExists != null)
                return UserErrors.AlreadyExists;

           var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var userToAdd = new User(request.FullName, request.Email, hashedPassword.Value , _dateTimeProvider.UtcNow , "" );
            
          
          
            var user =  await _usersRepository.AddAsync(userToAdd);

           var uploadedPhotoUrl =  await _fileService.UploadUserPhotoAsync(request.Photo, user.Id);

            if (uploadedPhotoUrl.IsError)
                return uploadedPhotoUrl.Errors;
           
            user.SetPhotoUrl(uploadedPhotoUrl.Value);

            var code = CodeGenerator.Generate8DigitCode();
            
          
            user.SetEmailConfirmationCode(code, _dateTimeProvider.UtcNow);
            
      
            await _unitOfWork.CompleteAsync();

            await _emailService.SendConfirmationToEamilAsync(user.Email, "Email confiramtion code", "confirm your email", code, User.EmailCodeExpiresAfterMinutes.ToString());

            return "please confirm your email to complete registeration process";
            
        }
    }
}
