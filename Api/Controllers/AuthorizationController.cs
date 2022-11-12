using Api.Bases;
using ApplicationCore.Constants;
using ApplicationCore.Cqrs.User.Create;
using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Login;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces;
using ApplicationCore.Resources;
using AutoMapper;
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
            ISettings settings) : base(mediator)
        {
            AuthorizationService = authorizationService;
            CookieService = cookieService;
            Mapper = mapper;
            PasswordHasher = passwordHasher;
            Settings = settings;
        }

        private IAuthenticationService AuthorizationService { get; }

        private ICookieService CookieService { get; }

        private IMapper Mapper { get; }

        private IPasswordHasher<UserDto> PasswordHasher { get; }

        private ISettings Settings { get; }

        [HttpGet("RefreshToken")]
        public async Task<ActionResult<AuthorizeDto>> GetToken()
        {
            var refreshToken = CookieService.GetCookie(CookieNameConst.RefreshToken);

            if (Guid.TryParse(refreshToken, out var guid))
            {
                var result = await AuthorizationService.GetAuthorizationAsync(guid);

                return Ok(result);
            }

            throw new Exception(ErrorMessages.WrongRefreshTokenFormat);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthorizeDto>> Login(LoginDto dto)
        {
            var result = await AuthorizationService.GetAuthorizationAsync(dto);
            var expireDays = Settings.GetRefreshTokenExpireDays();

            CookieService.AddCookie(CookieNameConst.RefreshToken, result.RefreshToken.ToString(), expireDays);

            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthorizeDto>> Register(RegisterDto registerDto)
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