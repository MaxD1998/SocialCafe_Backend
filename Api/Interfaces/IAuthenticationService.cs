using Api.Models;
using System.Net;

namespace Api.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthorizeDto> GetAuthorization(LoginDto loginDto);

        Task<AuthorizeDto> GetAuthorization(Guid refreshToken, IPAddress ipAddress);
    }
}