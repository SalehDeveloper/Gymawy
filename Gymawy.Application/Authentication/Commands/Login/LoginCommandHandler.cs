using ErrorOr;
using FluentValidation.Validators;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Application.Authentication.Commands.Login.Common;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.RefreshTokens;
using Gymawy.Domain.Users;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<LoginResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly AuthenticationOptions _authenticationOptions;


        public LoginCommandHandler(
            IUnitOfWork unitOfWork,
            IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService,
            IRefreshTokensRepository refreshTokensRepository,
            IOptions<AuthenticationOptions> authenticationOptions,
            IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _refreshTokensRepository = refreshTokensRepository;
            _authenticationOptions = authenticationOptions.Value;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            var user = await _usersRepository.FindAsync(x => x.Email == request.Email);

            if (user is null)
                return AuthenticaionErrors.InvalidCredentials;

            if (!user.IsActive)
                return UserErrors.Inactive;

            if (!_passwordHasher.IsCorrectPassword(request.Password, user.Password))
                return AuthenticaionErrors.InvalidCredentials;

            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            var refreshTokenToAdd = new Domain.RefreshTokens.RefreshToken
                (
                refreshToken,
                _dateTimeProvider.UtcNow.AddDays(_authenticationOptions.RefreshTokenDurationInDays),
                user.Id,
                _dateTimeProvider.UtcNow);
           

            await _refreshTokensRepository.AddAsync(refreshTokenToAdd);
             

            _jwtService.SetAccessTokenInCookies(accessToken);
            _jwtService.SetRefreshTokenInCookies(refreshToken);

           await _unitOfWork.CompleteAsync();

            var profileType = user.GetProfileType().Name;
            return new LoginResult(user.Id, user.Email, user.FullName, user.PhotoUrl , profileType);

        }


        
    }
    
}
