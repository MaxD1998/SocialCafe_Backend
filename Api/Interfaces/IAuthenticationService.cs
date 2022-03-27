using Api.Models;

namespace Api.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthorizeDto> GetAuthorization(LoginDto loginDto);

        Task<AuthorizeDto> GetAuthorization(Guid refreshToken);
    }
}