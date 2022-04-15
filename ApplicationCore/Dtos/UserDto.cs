using ApplicationCore.Bases;
using Domain.Entity;
using System.Text.Json.Serialization;

namespace ApplicationCore.Dtos
{
    public class UserDto : BaseDto
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string HashedPassword { get; set; }

        public bool IsDeleted { get; set; }

        public string LastName { get; set; }

        [JsonIgnore]
        public List<RefreshTokenEntity> RefreshTokens { get; set; }
    }
}