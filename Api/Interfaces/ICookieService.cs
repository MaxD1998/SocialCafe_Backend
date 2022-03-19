namespace Api.Interfaces
{
    public interface ICookieService
    {
        void AddCookie(string name, string value, int expire);

        public string GetCookie(string name);
    }
}