using ApplicationCore.Dtos.User;

namespace ApplicationCore.Dtos.RefreshToken;

public class RefreshTokenDto : RefreshTokenInputDto
{
    public Guid Id { get; set; }

    public UserDto User { get; set; }
}