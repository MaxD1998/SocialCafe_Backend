using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class RefreshTokenEntity : BaseEntity
{
    public DateTime CreationDate { get; set; }

    public DateTime ExpireDate { get; set; }

    public Guid Token { get; set; }

    [Column(Order = 3)]
    public Guid UserId { get; set; }

    #region Related data

    public UserEntity User { get; set; }

    #endregion Related data
}