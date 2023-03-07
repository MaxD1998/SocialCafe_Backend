using ApplicationCore.Dtos.RefreshToken;
using System.Text.Json.Serialization;

namespace ApplicationCore.Dtos.User;

public class UserDto : UserInputDto
{
    public Guid Id { get; set; }

    [JsonIgnore]
    public List<RefreshTokenDto> RefreshTokens { get; set; }
}