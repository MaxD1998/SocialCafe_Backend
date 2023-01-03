using System.Text.Json.Serialization;

namespace ApplicationCore.Dtos;

public class AuthorizeDto
{
    public int Id { get; set; }

    [JsonIgnore]
    public Guid RefreshToken { get; set; }

    public string Token { get; set; }

    public string Username { get; set; }
}