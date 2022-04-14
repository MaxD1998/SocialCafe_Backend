using System.Text.Json.Serialization;

namespace ApplicationCore.Dtos
{
    public class AuthorizeDto
    {
        [JsonIgnore]
        public Guid RefreshToken { get; set; }

        public string Token { get; set; }

        public string Username { get; set; }
    }
}