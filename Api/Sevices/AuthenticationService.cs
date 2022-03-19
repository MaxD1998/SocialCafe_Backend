using Api.Interfaces;
using Api.Models;
using AutoMapper;
using Common.Exceptions;
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
        public AuthenticationService(IHttpContextAccessor httpContext,
            IMapper mapper,
            IMediator mediator,
            IPasswordHasher<LoginDto> passwordHasher,
            ITokenGeneratorService tokenGeneratorService)
        {
            HttpContext = httpContext;
            Mediator = mediator;
            Mapper = mapper;
            PasswordHasher = passwordHasher;
            TokenGeneratorService = tokenGeneratorService;
        }

        public IHttpContextAccessor HttpContext { get; }

        private IMapper Mapper { get; }

        private IMediator Mediator { get; }

        private IPasswordHasher<LoginDto> PasswordHasher { get; }

        private ITokenGeneratorService TokenGeneratorService { get; }

        public async Task<AuthorizeDto> GetAuthorization(LoginDto dto)
        {
            var user = await Mediator.Send(new GetUserByEmailQuery(dto.Email));

            CheckLoginData(user, dto);

            return new AuthorizeDto()
            {
                Username = $"{user.FirstName} {user.LastName}",
                Token = TokenGeneratorService.GenerateJwt(user),
                RefreshToken = TokenGeneratorService.GenerateRefreshToken()
            };
        }

        public async Task<AuthorizeDto> GetAuthorization(Guid refreshToken, IPAddress ipAddress)
        {
            var user = await Mediator.Send(new GetUserByRefreshTokenAndIpAddressQuery(refreshToken, ipAddress));

            CheckRefreshToken(user);

            return new AuthorizeDto()
            {
                Username = $"{user.FirstName} {user.LastName}",
                Token = TokenGeneratorService.GenerateJwt(user),
            };
        }

        private void CheckLoginData(UserEntity entity, LoginDto dto)
        {
            var user = Mapper.Map<LoginDto>(entity);
            var passwordVerfication = PasswordHasher.VerifyHashedPassword(user, user?.Password, dto.Password);

            if (passwordVerfication == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizeException("Wrong email or passowrd");
            }
        }

        private void CheckRefreshToken(UserEntity entity)
        {
            if (entity is null)
            {
                throw new UnauthorizeException("Session has expired");
            }
        }
    }
}