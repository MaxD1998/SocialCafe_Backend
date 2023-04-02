using Domain.Bases;

namespace Domain.Entities;

public class FriendEntity : BaseEntity
{
    public Guid InviterId { get; set; }

    public Guid RecipientId { get; set; }

    #region Related data

    public UserEntity Inviter { get; set; }

    public UserEntity Recipient { get; set; }

    #endregion Related data
}