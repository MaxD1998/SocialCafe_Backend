﻿using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Sevices;

public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _accessor;

    public CookieService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public void AddCookie(string name, string value, int expire, bool httpOnly)
    {
        var cookie = new CookieOptions()
        {
            HttpOnly = httpOnly,
            Expires = DateTime.UtcNow.AddDays(expire),
            SameSite = SameSiteMode.None,
            Secure = true,
        };

        _accessor.HttpContext.Response.Cookies.Append(name, value, cookie);
    }

    public string GetCookie(string name)
    {
        var isValue = _accessor.HttpContext.Request.Cookies.TryGetValue(name, out var value);

        if (isValue)
        {
            return value;
        }

        return string.Empty;
    }

    public void RemoveCookie(string name)
        => _accessor.HttpContext.Response.Cookies.Delete(name);
}