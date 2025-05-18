using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using Gymawy.Domain.Users;
using MediatR;

namespace Gymawy.Application.Profiles.Commands.CreateAdminProfile
{
    public class CreateAdminProfileCommandHandler : IRequestHandler<CreateAdminProfileCommand, ErrorOr<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUsersRepository _usersRepository;
        private readonly IAdminsRepository _adminsRepository;

        public CreateAdminProfileCommandHandler(
            IUnitOfWork unitOfWork,
            IUserContext userContext,
            IDateTimeProvider dateTimeProvider,
            IUsersRepository usersRepository,
            IAdminsRepository adminsRepository)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _dateTimeProvider = dateTimeProvider;
            _usersRepository = usersRepository;
            _adminsRepository = adminsRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateAdminProfileCommand request, CancellationToken cancellationToken)
        {
            var userId =  _userContext.UserId;

            var user  = await _usersRepository.FindAsync(x=> x.Id == userId , null , cancellationToken);

            if (user == null) 
                return UserErrors.NotFound;

           
            var adminId = user.CreateAdminProfile();

            if (adminId.IsError)
                return adminId.Errors;

            var admin = new Admin(user.Id , _dateTimeProvider.UtcNow , null , adminId.Value);

            var result = await _adminsRepository.AddAsync(admin , cancellationToken);

           await _unitOfWork.CompleteAsync();

            return admin.Id;

        }
    }
}
