namespace ApplicationCore.Interfaces
{
    public interface ICookieService
    {
        void AddCookie(string name, string value, int expire);

        string GetCookie(string name);
    }
}