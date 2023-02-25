namespace ApplicationCore.Interfaces;

public interface ICookieService
{
    void AddCookie(string name, string value, int expire, bool httpOnly);

    string GetCookie(string name);

    public void RemoveCookie(string name);
}