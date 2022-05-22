using ApplicationCore.Cqrs.User.Create;
using ApplicationCore.Cqrs.User.Get;
using ApplicationCore.Cqrs.User.Update;
using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Login;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Dtos.User;
using ApplicationCore.Exceptions;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using ApplicationCore.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace ApplicationCore.Sevices
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService(IHttpContextAccessor accessor,
            IMediator mediator,
            IPasswordHasher<UserDto> passwordHasher,
            ISettings settings,
            ITokenGeneratorService tokenGeneratorService)
        {
            Accessor = accessor;
            Mediator = mediator;
            PasswordHasher = passwordHasher;
            Settings = settings;
            TokenGeneratorService = tokenGeneratorService;
        }

        private IHttpContextAccessor Accessor { get; }

        private IMediator Mediator { get; }

        private IPasswordHasher<UserDto> PasswordHasher { get; }

        private ISettings Settings { get; }

        private ITokenGeneratorService TokenGeneratorService { get; }

        private IPAddress UserRemoteIp => Accessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4();

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
            var user = await Mediator.Send(new GetUserByRefreshTokenAndIpAddressQuery(refreshToken, UserRemoteIp));

            user.ThrowIfNull(new UnauthorizeException(ErrorMessages.SessionWasExpired));

            return new AuthorizeDto()
            {
                Username = $"{user.FirstName} {user.LastName}",
                Token = TokenGeneratorService.GenerateJwt(user),
            };
        }

        private async Task<Guid> AddOrUpdateRefreshTokenAsync(UserDto dto)
        {
            var refreshToken = dto.RefreshTokens?
                .FirstOrDefault(x => x.RemoteAddress.Equals(UserRemoteIp));

            var newRefreshToken = new RefreshTokenInputDto()
            {
                CreationDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddDays(Settings.GetRefreshTokenExpireDays()),
                RemoteAddress = UserRemoteIp,
                Token = TokenGeneratorService.GenerateRefreshToken(),
            };

            if (refreshToken is null)
            {
                var createUserResponse = await Mediator.Send(new CreateRefreshTokenCommand(dto.Id, newRefreshToken));
                var createdRefreshToken = createUserResponse.RefreshTokens
                    .LastOrDefault();

                return createdRefreshToken.Token;
            }

            await Mediator.Send(new UpdateRefreshTokenCommand(dto.Id, refreshToken.Id, newRefreshToken));

            return newRefreshToken.Token;
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

        private async Task<Guid> GetRefreshTokenAsync(UserDto dto)
        {
            var refreshToken = dto.RefreshTokens?
                .FirstOrDefault(x => x.RemoteAddress.Equals(UserRemoteIp)
                    && x.ExpireDate >= DateTime.UtcNow);

            if (refreshToken is null)
            {
                return await AddOrUpdateRefreshTokenAsync(dto);
            }

            return refreshToken.Token;
        }
    }
}