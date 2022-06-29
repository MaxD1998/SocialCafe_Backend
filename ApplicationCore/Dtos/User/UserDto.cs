using ApplicationCore.Dtos.RefreshToken;
using System.Text.Json.Serialization;

namespace ApplicationCore.Dtos.User
{
    public class UserDto : UserInputDto
    {
        [JsonIgnore]
        public string HashedPassword { get; set; }

        public int Id { get; set; }

        [JsonIgnore]
        public List<RefreshTokenDto> RefreshTokens { get; set; }
    }
}