﻿using Api.Bases;
using ApplicationCore.Constants;
using ApplicationCore.Cqrs.User.Create;
using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Login;
using ApplicationCore.Dtos.User;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Resources;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[AllowAnonymous]
public class AuthorizationController : BaseApiController
{
    private readonly IAuthenticationService _authorizationService;
    private readonly ICookieService _cookieService;
    private readonly IMapper _mapper;
    private readonly ISettings _settings;

    public AuthorizationController(
        IAuthenticationService authorizationService,
        ICookieService cookieService,
        IMapper mapper,
        ISettings settings,
        IMediator mediator) : base(mediator)
    {
        _authorizationService = authorizationService;
        _cookieService = cookieService;
        _mapper = mapper;
        _settings = settings;
    }

    [HttpGet("RefreshToken")]
    public async Task<ActionResult<AuthorizeDto>> GetTokenAsync()
    {
        var refreshToken = _cookieService.GetCookie(CookieNameConst.RefreshToken);

        if (!Guid.TryParse(refreshToken, out var guid))
            throw new ForbiddenException(ErrorMessages.WrongRefreshTokenFormat);

        var result = await _authorizationService.GetAuthorizationAsync(guid);

        return Ok(result);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthorizeDto>> LoginAsync([FromBody] LoginDto dto)
    {
        var result = await _authorizationService.GetAuthorizationAsync(dto);
        var expireDays = _settings.GetRefreshTokenExpireDays();

        _cookieService.AddCookie(CookieNameConst.RefreshToken, result.RefreshToken.ToString(), expireDays, true);

        return Ok(result);
    }

    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        _cookieService.RemoveCookie(CookieNameConst.RefreshToken);
        return Ok();
    }

    [HttpPost("Register")]
    public async Task<ActionResult<AuthorizeDto>> RegisterAsync([FromBody] RegisterDto registerDto)
    {
        var dto = _mapper.Map<UserInputDto>(registerDto);
        var user = await _mediator.Send(new CreateUserCommand(dto));
        var result = await _authorizationService.GetAuthorizationAsync(user, registerDto.Password);
        var expireDays = _settings.GetRefreshTokenExpireDays();

        _cookieService.AddCookie(CookieNameConst.RefreshToken, result.RefreshToken.ToString(), expireDays, true);

        return Ok(result);
    }
}