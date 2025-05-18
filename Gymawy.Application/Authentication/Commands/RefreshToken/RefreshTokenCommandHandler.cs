using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Application.Authentication.Commands.Login.Common;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.RefreshTokens;
using Gymawy.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ErrorOr<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly AuthenticationOptions _authenticationOptions;

        public RefreshTokenCommandHandler(
            IUnitOfWork unitOfWork,
            IRefreshTokensRepository refreshTokensRepository,
            IUsersRepository usersRepository,
            IJwtService jwtService,
            IHttpContextAccessor contextAccessor,
            IDateTimeProvider dateTimeProvider,
            IOptions<AuthenticationOptions>authenticationOptions)
        {
            _unitOfWork = unitOfWork;
            _refreshTokensRepository = refreshTokensRepository;
            _usersRepository = usersRepository;
            _jwtService = jwtService;
            _contextAccessor = contextAccessor;
            _dateTimeProvider = dateTimeProvider;
            _authenticationOptions = authenticationOptions.Value;
        }

        public async Task<ErrorOr<string>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var context = _contextAccessor.HttpContext;

            if (context == null)
                throw new ArgumentNullException("context is unavailable");

            var refreshToken = context.Request.Cookies[_jwtService.RefreshTokenCookieName];

            if (string.IsNullOrEmpty(refreshToken))
                return AuthenticaionErrors.InvalidRefreshToken;

            var token =await _refreshTokensRepository.FindAsync(x => x.Token == refreshToken);

            if (token == null || token.IsExpired || !token.IsActive)
                return AuthenticaionErrors.InvalidRefreshToken;

            var user =await _usersRepository.FindAsync( u => u.Id == token.UserId);

            token.Revoke();

            var newToken = _jwtService.GenerateRefreshToken();


            
            var refreshTokenToAdd =new Domain.RefreshTokens.RefreshToken
                (
                  newToken,
                  _dateTimeProvider.UtcNow.AddDays(_authenticationOptions.RefreshTokenDurationInDays),
                  user.Id,
                 _dateTimeProvider.UtcNow
                );

            var newAccessToken = _jwtService.GenerateAccessToken(user);

            await  _refreshTokensRepository.AddAsync( refreshTokenToAdd  , cancellationToken);
            await _unitOfWork.CompleteAsync();

            _jwtService.SetAccessTokenInCookies(newAccessToken);
            _jwtService.SetRefreshTokenInCookies(newToken);

            return $"Token refreshed";


        }
    }
}
