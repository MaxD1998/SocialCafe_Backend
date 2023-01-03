using ApplicationCore.Dtos.RefreshToken;
using System.Text.Json.Serialization;

namespace ApplicationCore.Dtos.User;

public class UserDto : UserInputDto
{
    public int Id { get; set; }

    [JsonIgnore]
    public List<RefreshTokenDto> RefreshTokens { get; set; }
}