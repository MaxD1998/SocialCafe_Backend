using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class User : BaseEntity
    {
        [Column(Order = 3)]
        public string Email { get; set; }

        [Column(Order = 1)]
        public string FirstName { get; set; }

        [Column(Order = 4)]
        public string HashPassword { get; set; }

        [Column(Order = 2)]
        public string LastName { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}