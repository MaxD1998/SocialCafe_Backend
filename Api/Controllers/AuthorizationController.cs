using Api.Bases;
using ApplicationCore.Cqrs.User.Create;
using ApplicationCore.Dtos;
using ApplicationCore.Interfaces;
using AutoMapper;
using Common.Constants;
using Common.Interfaces;
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
            IPasswordHasher<UserDto> passwordHasher,
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

        private IPasswordHasher<UserDto> PasswordHasher { get; }

        private ISettings Settings { get; }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> GetToken()
        {
            var refreshToken = CookieService.GetCookie(CookieNameConst.RefreshToken);

            if (Guid.TryParse(refreshToken, out var guid))
            {
                var result = await AuthorizationService.GetAuthorizationAsync(guid);

                return Ok(result);
            }

            throw new Exception(ExceptionMessageConst.WrongRefreshTokenFormat);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await AuthorizationService.GetAuthorizationAsync(dto);
            var expireDays = Settings.GetRefreshTokenExpireDays();

            CookieService.AddCookie(CookieNameConst.RefreshToken, result.RefreshToken.ToString(), expireDays);

            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var dto = Mapper.Map<UserDto>(registerDto);

            dto.HashedPassword = PasswordHasher.HashPassword(dto, registerDto.Password);

            var user = await Mediator.Send(new CreateUserCommand(dto));
            var result = await AuthorizationService.GetAuthorizationAsync(user, registerDto.Password);
            var expireDays = Settings.GetRefreshTokenExpireDays();

            CookieService.AddCookie(CookieNameConst.RefreshToken, result.RefreshToken.ToString(), expireDays);

            return Ok(result);
        }
    }
}