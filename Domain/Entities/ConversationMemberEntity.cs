using Domain.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ConversationMemberEntity : BaseEntity
{
    [Column(Order = 3)]
    public Guid ConversationId { get; set; }

    public string Nick { get; set; }

    [Column(Order = 4)]
    public Guid UserId { get; set; }

    #region Related data

    public ConversationEntity Conversation { get; set; }

    public UserEntity User { get; set; }

    #endregion Related data
}