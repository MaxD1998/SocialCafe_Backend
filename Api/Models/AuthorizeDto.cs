using System.Text.Json.Serialization;

namespace Api.Models
{
    public class AuthorizeDto
    {
        [JsonIgnore]
        public string RefreshToken { get; set; }

        public string Token { get; set; }

        public string Username { get; set; }
    }
}