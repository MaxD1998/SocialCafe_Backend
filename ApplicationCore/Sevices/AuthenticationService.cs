using ApplicationCore.Cqrs.RefreshToken.Create;
using ApplicationCore.Cqrs.RefreshToken.Get;
using ApplicationCore.Cqrs.RefreshToken.Update;
using ApplicationCore.Cqrs.User.Get;
using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Login;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Dtos.User;
using ApplicationCore.Exceptions;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using ApplicationCore.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Sevices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMediator _mediator;
        private readonly IPasswordHasher<UserDto> _passwordHasher;
        private readonly ISettings _settings;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public AuthenticationService(IMediator mediator,
                                            IPasswordHasher<UserDto> passwordHasher,
            ISettings settings,
            ITokenGeneratorService tokenGeneratorService)
        {
            _mediator = mediator;
            _passwordHasher = passwordHasher;
            _settings = settings;
            _tokenGeneratorService = tokenGeneratorService;
        }

        public async Task<AuthorizeDto> GetAuthorizationAsync(LoginDto dto)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery(dto.Email));

            return await GetAuthorizationAsync(user, dto.Password);
        }

        public async Task<AuthorizeDto> GetAuthorizationAsync(UserDto dto, string password)
        {
            CheckLoginData(dto, password);

            var refreshToken = await AddOrUpdateRefreshTokenAsync(dto.Id);

            return new AuthorizeDto()
            {
                Id = dto.Id,
                Username = $"{dto.FirstName} {dto.LastName}",
                Token = _tokenGeneratorService.GenerateJwt(dto),
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthorizeDto> GetAuthorizationAsync(Guid token)
        {
            var refreshToken = await _mediator.Send(new GetRefereshTokenByTokenQuery(token));

            refreshToken.ThrowIfNull(new ForbiddenException(ErrorMessages.SessionWasExpired));

            var user = refreshToken.User;

            return new AuthorizeDto()
            {
                Id = user.Id,
                Username = $"{user.FirstName} {user.LastName}",
                Token = _tokenGeneratorService.GenerateJwt(user),
            };
        }

        private async Task<Guid> AddOrUpdateRefreshTokenAsync(int userId)
        {
            var inputRefreshToken = new RefreshTokenInputDto()
            {
                CreationDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddDays(_settings.GetRefreshTokenExpireDays()),
                Token = _tokenGeneratorService.GenerateRefreshToken(),
                UserId = userId,
            };

            var oldRefreshToken = await _mediator.Send(new GetRefreshTokenByUserIdQuery(userId));

            if (oldRefreshToken is null)
            {
                var newRefreshToken = await _mediator.Send(new CreateRefreshTokenCommand(inputRefreshToken));
                return newRefreshToken.Token;
            }

            var updatedRefreshToken = await _mediator.Send(new UpdateRefreshTokenCommand(oldRefreshToken.Id, inputRefreshToken));
            return updatedRefreshToken.Token;
        }

        private void CheckLoginData(UserDto dto, string password)
        {
            dto.ThrowIfNull(new UnauthorizeException(ErrorMessages.WrongEmailOrPassword));

            var passwordVerfication = _passwordHasher.VerifyHashedPassword(dto, dto?.HashedPassword, password);

            if (passwordVerfication == PasswordVerificationResult.Failed)
                throw new UnauthorizeException(ErrorMessages.WrongEmailOrPassword);
        }
    }
}