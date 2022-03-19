using Api.Constants;
using Api.Controllers.Common;
using Api.Interfaces;
using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    public class AuthorizationController : BaseApiController
    {
        public AuthorizationController(IAuthenticationService authorizationService,
            ICookieService cookieService,
            IMediator mediator,
            ISettings settings)
        {
            AuthorizationService = authorizationService;
            CookieService = cookieService;
            Mediator = mediator;
            Settings = settings;
        }

        public IMediator Mediator { get; }

        private IAuthenticationService AuthorizationService { get; }

        private ICookieService CookieService { get; }

        private ISettings Settings { get; }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            var result = await AuthorizationService.GetAuthorization(dto);
            var expireDays = Settings.GetRefreshTokenExpireDays();

            CookieService.AddCookie(CookieNameConstant.RefreshToken, result.RefreshToken, expireDays);

            return Ok(result);
        }

        [HttpPost("refreshtoken")]
        public async Task<ActionResult> Login()
        {
            //return Ok(await Mediator.Send(new GetUserByEmailQuery("mamich1998@gmail.com")));
            var x = AppDomain.CurrentDomain.GetAssemblies();
            return Ok(x.Where(x => x.FullName.Contains("null")).Select(x => x.FullName));
        }
    }
}