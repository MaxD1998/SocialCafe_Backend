using Api.Interfaces;

namespace Api.Sevices
{
    public class CookieService : ICookieService
    {
        public CookieService(IHttpContextAccessor accessor)
        {
            Accessor = accessor;
        }

        private IHttpContextAccessor Accessor { get; }

        public void AddCookie(string name, string value, int expire)
        {
            var cookie = new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(expire)
            };

            Accessor.HttpContext.Response.Cookies.Append(name, value, cookie);
        }

        public string GetCookie(string name)
        {
            var isValue = Accessor.HttpContext.Request.Cookies.TryGetValue(name, out var value);

            if (isValue)
            {
                return value;
            }

            return string.Empty;
        }
    }
}