﻿using Api.Interfaces;
using Api.Models;
using AutoMapper;
using Common.Constants;
using Common.Exceptions;
using Common.Extensions;
using Cqrs.Api.RefreshToken.Create;
using Cqrs.Api.RefreshToken.Update;
using Cqrs.Api.User.Get.GetByEmail;
using Cqrs.Api.User.Get.GetByRefreshTokenAndIpAddress;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Api.Sevices
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService(IHttpContextAccessor accessor,
            IMapper mapper,
            IMediator mediator,
            IPasswordHasher<UserEntity> passwordHasher,
            ISettings settings,
            ITokenGeneratorService tokenGeneratorService)
        {
            Accessor = accessor;
            Mediator = mediator;
            Mapper = mapper;
            PasswordHasher = passwordHasher;
            Settings = settings;
            TokenGeneratorService = tokenGeneratorService;
        }

        private IHttpContextAccessor Accessor { get; }

        private IMapper Mapper { get; }

        private IMediator Mediator { get; }

        private IPasswordHasher<UserEntity> PasswordHasher { get; }

        private ISettings Settings { get; }

        private ITokenGeneratorService TokenGeneratorService { get; }

        private IPAddress UserRemoteIp => Accessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4();

        public async Task<AuthorizeDto> GetAuthorizationAsync(LoginDto dto)
        {
            var user = await Mediator.Send(new GetUserByEmailQuery(dto.Email));

            return await GetAuthorizationAsync(user, dto.Password);
        }

        public async Task<AuthorizeDto> GetAuthorizationAsync(UserEntity entity, string password)
        {
            CheckLoginData(entity, password);

            var refreshToken = await GetRefreshTokenAsync(entity);

            return new AuthorizeDto()
            {
                Username = $"{entity.FirstName} {entity.LastName}",
                Token = TokenGeneratorService.GenerateJwt(entity),
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthorizeDto> GetAuthorizationAsync(Guid refreshToken)
        {
            var user = await Mediator.Send(new GetUserByRefreshTokenAndIpAddressQuery(refreshToken, UserRemoteIp));

            user.ThrowIfNull(new UnauthorizeException(ExceptionMessageConst.SessionWasExpired));

            return new AuthorizeDto()
            {
                Username = $"{user.FirstName} {user.LastName}",
                Token = TokenGeneratorService.GenerateJwt(user),
            };
        }

        private async Task<Guid> AddOrUpdateRefreshTokenAsync(UserEntity entity)
        {
            var refreshToken = entity.RefreshTokens
                .FirstOrDefault(x => x.RemoteAddress.Equals(UserRemoteIp));

            var newRefreshToken = new RefreshTokenEntity()
            {
                CreationDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddDays(Settings.GetRefreshTokenExpireDays()),
                RemoteAddress = UserRemoteIp,
                Token = TokenGeneratorService.GenerateRefreshToken(),
            };

            if (refreshToken is null)
            {
                var createdRefreshToken = await Mediator.Send(new CreateRefreshTokenCommand(entity.Id, newRefreshToken));

                return createdRefreshToken.Token;
            }

            var updatedRefreshToken = await Mediator.Send(new UpdateRefreshTokenCommand(entity.Id, newRefreshToken));

            return updatedRefreshToken.Token;
        }

        private void CheckLoginData(UserEntity entity, string password)
        {
            entity.ThrowIfNull(new UnauthorizeException(ExceptionMessageConst.WrongEmailOrPassword));

            var user = Mapper.Map<UserEntity>(entity);
            var passwordVerfication = PasswordHasher.VerifyHashedPassword(entity, entity?.HashedPassword, password);

            if (passwordVerfication == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizeException(ExceptionMessageConst.WrongEmailOrPassword);
            }
        }

        private async Task<Guid> GetRefreshTokenAsync(UserEntity entity)
        {
            var refreshToken = entity.RefreshTokens
                .FirstOrDefault(x => x.RemoteAddress.Equals(UserRemoteIp)
                    && x.ExpireDate >= DateTime.UtcNow);

            if (refreshToken is null)
            {
                return await AddOrUpdateRefreshTokenAsync(entity);
            }

            return refreshToken.Token;
        }
    }
}