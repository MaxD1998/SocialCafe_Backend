using Api.Interfaces;
using Domain.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Sevices
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        public TokenGeneratorService(ISettings settings)
        {
            Settings = settings;
        }

        public ISettings Settings { get; }

        public string GenerateJwt(UserEntity user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.GetJwtKey()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Settings.GetJwtExpireMinutes()),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var getToken = tokenHandler.WriteToken(token);

            return getToken;
        }

        public Guid GenerateRefreshToken()
        {
            return Guid.NewGuid();
        }
    }
}