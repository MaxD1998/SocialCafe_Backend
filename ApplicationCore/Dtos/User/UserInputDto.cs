using System.Text.Json.Serialization;

namespace ApplicationCore.Dtos.User;

public class UserInputDto
{
    [JsonIgnore]
    public string ConnectionId { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    [JsonIgnore]
    public string HashedPassword { get; set; }

    public string LastName { get; set; }
}