using ApplicationCore.Dtos.User;

namespace ApplicationCore.Dtos.RefreshToken
{
    public class RefreshTokenDto : RefreshTokenInputDto
    {
        public int Id { get; set; }

        public UserDto User { get; set; }
    }
}