using Domain.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class MessageEntity : BaseEntity
{
    [Column(Order = 3)]
    public Guid ConversationId { get; set; }

    [Column(Order = 5)]
    public string Text { get; set; }

    [Column(Order = 4)]
    public Guid UserId { get; set; }

    #region Related data

    public ConversationEntity Converstaion { get; set; }

    public UserEntity User { get; set; }

    #endregion Related data
}