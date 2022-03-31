using System.Text.Json.Serialization;

namespace Api.Models
{
    public class AuthorizeDto
    {
        [JsonIgnore]
        public Guid RefreshToken { get; set; }

        public string Token { get; set; }

        public string Username { get; set; }
    }
}