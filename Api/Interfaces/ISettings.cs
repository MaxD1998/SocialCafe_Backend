namespace Api.Interfaces
{
    public interface ISettings
    {
        int GetJwtExpireMinutes();

        string GetJwtKey();

        int GetRefreshTokenExpireDays();
    }
}