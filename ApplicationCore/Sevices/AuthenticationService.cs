using ApplicationCore.Cqrs.User.Create;
using ApplicationCore.Cqrs.User.Delete;
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
        public AuthenticationService(IMediator mediator,
            IPasswordHasher<UserDto> passwordHasher,
            ISettings settings,
            ITokenGeneratorService tokenGeneratorService)
        {
            Mediator = mediator;
            PasswordHasher = passwordHasher;
            Settings = settings;
            TokenGeneratorService = tokenGeneratorService;
        }

        private IMediator Mediator { get; }

        private IPasswordHasher<UserDto> PasswordHasher { get; }

        private ISettings Settings { get; }

        private ITokenGeneratorService TokenGeneratorService { get; }

        public async Task<AuthorizeDto> GetAuthorizationAsync(LoginDto dto)
        {
            var user = await Mediator.Send(new GetUserByEmailQuery(dto.Email));

            return await GetAuthorizationAsync(user, dto.Password);
        }

        public async Task<AuthorizeDto> GetAuthorizationAsync(UserDto dto, string password)
        {
            CheckLoginData(dto, password);

            var refreshToken = await GetRefreshTokenAsync(dto);

            return new AuthorizeDto()
            {
                Username = $"{dto.FirstName} {dto.LastName}",
                Token = TokenGeneratorService.GenerateJwt(dto),
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthorizeDto> GetAuthorizationAsync(Guid refreshToken)
        {
            var user = await Mediator.Send(new GetUserByRefreshTokenQuery(refreshToken));

            user.ThrowIfNull(new UnauthorizeException(ErrorMessages.SessionWasExpired));

            return new AuthorizeDto()
            {
                Username = $"{user.FirstName} {user.LastName}",
                Token = TokenGeneratorService.GenerateJwt(user),
            };
        }

        private async Task<Guid> AddRefreshTokenAsync(UserDto dto)
        {
            var newRefreshToken = new RefreshTokenInputDto()
            {
                CreationDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddDays(Settings.GetRefreshTokenExpireDays()),
                Token = TokenGeneratorService.GenerateRefreshToken(),
            };

            var createUserResponse = await Mediator.Send(new CreateRefreshTokenCommand(dto.Id, newRefreshToken));
            var createdRefreshToken = createUserResponse.RefreshTokens
                .LastOrDefault();

            return createdRefreshToken.Token;
        }

        private void CheckLoginData(UserDto dto, string password)
        {
            dto.ThrowIfNull(new UnauthorizeException(ErrorMessages.WrongEmailOrPassword));

            var passwordVerfication = PasswordHasher.VerifyHashedPassword(dto, dto?.HashedPassword, password);

            if (passwordVerfication == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizeException(ErrorMessages.WrongEmailOrPassword);
            }
        }

        private async Task<bool> DeleteOldRefreshTokens(UserDto dto)
        {
            var oldRefreshTokens = dto.RefreshTokens
                .Where(x => x.ExpireDate < DateTime.UtcNow)
                .ToList();

            return await Mediator.Send(new DeleteRefreshTokensCommand(dto.Id, oldRefreshTokens));
        }

        private async Task<Guid> GetRefreshTokenAsync(UserDto dto)
        {
            await DeleteOldRefreshTokens(dto);
            return await AddRefreshTokenAsync(dto);
        }
    }
}