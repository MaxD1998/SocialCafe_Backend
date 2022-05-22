using ApplicationCore.Dtos.User;

namespace ApplicationCore.Interfaces
{
    public interface ITokenGeneratorService
    {
        string GenerateJwt(UserDto user);

        Guid GenerateRefreshToken();
    }
}