using Api.Interfaces;

namespace Api.Settings
{
    public class MainSettings : ISettings
    {
        public JwtSettings JwtSettings { get; set; }

        public RefreshTokenSettings RefreshTokenSettings { get; set; }

        public int GetJwtExpireMinutes()
        {
            if (JwtSettings is null)
            {
                return default;
            }

            return JwtSettings.ExpireTime;
        }

        public string GetJwtKey()
        {
            if (JwtSettings is null)
            {
                return default;
            }

            return JwtSettings.JwtKey;
        }

        public int GetRefreshTokenExpireDays()
        {
            if (RefreshTokenSettings is null)
            {
                return default;
            }

            return RefreshTokenSettings.ExpireTime;
        }
    }
}