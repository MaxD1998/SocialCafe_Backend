using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Domain.Entity
{
    [Owned]
    public class RefreshTokenEntity
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