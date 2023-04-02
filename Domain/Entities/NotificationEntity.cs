using Domain.Bases;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class NotificationEntity : BaseEntity
{
    public bool IsRead { get; set; }

    [Column(Order = 4)]
    public Guid RecipientId { get; set; }

    public NotificationType Type { get; set; }

    [Column(Order = 3)]
    public Guid UserId { get; set; }

    #region Related data

    public UserEntity Recipient { get; set; }

    public UserEntity User { get; set; }

    #endregion Related data
}