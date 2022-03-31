using Api.Models;
using Domain.Entity;

namespace Api.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthorizeDto> GetAuthorizationAsync(LoginDto loginDto);

        Task<AuthorizeDto> GetAuthorizationAsync(UserEntity entity, string password);

        Task<AuthorizeDto> GetAuthorizationAsync(Guid refreshToken);
    }
}