using Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Index(nameof(Email), IsUnique = true)]
    [Table("User")]
    public class UserEntity : BaseEntity
    {
        [Column(Order = 3)]
        public string Email { get; set; }

        [Column(Order = 1)]
        public string FirstName { get; set; }

        [Column(Order = 4)]
        public string HashedPassword { get; set; }

        public bool IsDeleted { get; set; }

        [Column(Order = 2)]
        public string LastName { get; set; }

        public List<RefreshTokenEntity> RefreshTokens { get; set; }
    }
}