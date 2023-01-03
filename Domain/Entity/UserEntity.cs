using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class UserEntity : BaseEntity
{
    public List<CommentEntity> Comments { get; set; }

    [Column(Order = 5)]
    public string ConnectionId { get; set; }

    [Column(Order = 3)]
    public string Email { get; set; }

    [Column(Order = 1)]
    public string FirstName { get; set; }

    [Column(Order = 4)]
    public string HashedPassword { get; set; }

    [Column(Order = 2)]
    public string LastName { get; set; }

    #region Related data

    public ICollection<FriendEntity> InvitedByUsers { get; set; }

    public ICollection<FriendEntity> InvitedUsers { get; set; }

    public ICollection<PostEntity> Posts { get; set; }

    public ICollection<MessageEntity> RecieveMessages { get; set; }

    public RefreshTokenEntity RefreshToken { get; set; }

    public ICollection<MessageEntity> SendMessages { get; set; }

    #endregion Related data
}