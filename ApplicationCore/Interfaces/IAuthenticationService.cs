using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Login;
using ApplicationCore.Dtos.User;

namespace ApplicationCore.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthorizeDto> GetAuthorizationAsync(LoginDto loginDto);

        Task<AuthorizeDto> GetAuthorizationAsync(UserDto entity, string password);

        Task<AuthorizeDto> GetAuthorizationAsync(Guid refreshToken);
    }
}