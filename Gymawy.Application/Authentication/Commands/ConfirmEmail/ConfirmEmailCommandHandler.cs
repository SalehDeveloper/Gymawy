using ErrorOr;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ErrorOr<string>>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmEmailCommandHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork)
        {
            _usersRepository = usersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.FindAsync(user => user.Email == request.Email, null , cancellationToken);

            if (user == null)
                return UserErrors.NotFound;

          var result =   user.ConfirmEmail(request.code);
             
            if (result.IsError)
                return result.Errors;

            await _unitOfWork.CompleteAsync();
            return $"email confirmed successfully , now you can login to your account";



        }
    }
}
