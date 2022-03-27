using Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Domain.Entity
{
    [Owned]
    [Table("RefreshToken")]
    public class RefreshTokenEntity : BaseEntity
    {
        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

        [Required]
        public IPAddress RemoteAddress { get; set; }

        [Required]
        public Guid Token { get; set; }
    }
}