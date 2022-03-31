using Api.Common;
using Api.Interfaces;
using Api.Models;
using AutoMapper;
using Common.Constants;
using Cqrs.Api.User.Create;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    public class AuthorizationController : BaseApiController
    {
        public AuthorizationController(
            IAuthenticationService authorizationService,
            ICookieService cookieService,
            IMapper mapper,
            IMediator mediator,
            IPasswordHasher<UserEntity> passwordHasher,
            ISettings settings)
        {
            AuthorizationService = authorizationService;
            CookieService = cookieService;
            Mapper = mapper;
            Mediator = mediator;
            PasswordHasher = passwordHasher;
            Settings = settings;
        }

        private IAuthenticationService AuthorizationService { get; }

        private ICookieService CookieService { get; }

        private IMapper Mapper { get; }

        private IMediator Mediator { get; }

        private IPasswordHasher<UserEntity> PasswordHasher { get; }

        private ISettings Settings { get; }

        [HttpPost("refreshtoken")]
        public async Task<ActionResult> GetToken()
        {
            var refreshToken = CookieService.GetCookie(CookieNameConst.RefreshToken);

            if (Guid.TryParse(refreshToken, out var guid))
            {
                var result = await AuthorizationService.GetAuthorizationAsync(guid);

                return Ok(result);
            }

            throw new Exception(ExceptionMessageConst.WrongRefreshTokenFormat);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            var result = await AuthorizationService.GetAuthorizationAsync(dto);
            var expireDays = Settings.GetRefreshTokenExpireDays();

            CookieService.AddCookie(CookieNameConst.RefreshToken, result.RefreshToken.ToString(), expireDays);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto dto)
        {
            var entity = Mapper.Map<UserEntity>(dto);

            entity.HashedPassword = PasswordHasher.HashPassword(entity, dto.Password);

            var user = await Mediator.Send(new CreateUserCommand(entity));
            var result = await AuthorizationService.GetAuthorizationAsync(user, dto.Password);
            var expireDays = Settings.GetRefreshTokenExpireDays();

            CookieService.AddCookie(CookieNameConst.RefreshToken, result.RefreshToken.ToString(), expireDays);

            return Ok(result);
        }
    }
}