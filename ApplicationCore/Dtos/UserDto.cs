using ApplicationCore.Bases;
using Domain.Entity;

namespace ApplicationCore.Dtos
{
    public class UserDto : BaseDto
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string HashedPassword { get; set; }

        public bool IsDeleted { get; set; }

        public string LastName { get; set; }

        public List<RefreshTokenEntity> RefreshTokens { get; set; }
    }
}