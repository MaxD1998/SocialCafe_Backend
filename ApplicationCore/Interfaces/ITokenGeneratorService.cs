using ApplicationCore.Dtos;

namespace ApplicationCore.Interfaces
{
    public interface ITokenGeneratorService
    {
        string GenerateJwt(UserDto user);

        Guid GenerateRefreshToken();
    }
}