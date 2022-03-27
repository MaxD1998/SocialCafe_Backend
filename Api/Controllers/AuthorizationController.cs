using Api.Common;
using Api.Interfaces;
using Api.Models;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    public class AuthorizationController : BaseApiController
    {
        public AuthorizationController(IAuthenticationService authorizationService,
            ICookieService cookieService,
            ISettings settings)
        {
            AuthorizationService = authorizationService;
            CookieService = cookieService;
            Settings = settings;
        }

        private IAuthenticationService AuthorizationService { get; }

        private ICookieService CookieService { get; }

        private ISettings Settings { get; }

        [HttpPost("refreshtoken")]
        public async Task<ActionResult> GetToken()
        {
            var refreshToken = CookieService.GetCookie(CookieNameConst.RefreshToken);

            if (Guid.TryParse(refreshToken, out var guid))
            {
                var result = await AuthorizationService.GetAuthorization(guid);

                return Ok(result);
            }

            throw new Exception(ExceptionMessageConst.WrongRefreshTokenFormat);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            var result = await AuthorizationService.GetAuthorization(dto);
            var expireDays = Settings.GetRefreshTokenExpireDays();

            CookieService.AddCookie(CookieNameConst.RefreshToken, result.RefreshToken.ToString(), expireDays);

            return Ok(result);
        }
    }
}