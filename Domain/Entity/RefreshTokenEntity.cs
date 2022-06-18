using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("RefreshToken")]
    public class RefreshTokenEntity : BaseEntity
    {
        public DateTime CreationDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public Guid Token { get; set; }
    }
}