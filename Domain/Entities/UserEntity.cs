using Domain.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class UserEntity : BaseEntity
{
    [Column(Order = 7)]
    public string ConnectionId { get; set; }

    [Column(Order = 3)]
    public string Email { get; set; }

    [Column(Order = 5)]
    public string FirstName { get; set; }

    [Column(Order = 6)]
    public string HashedPassword { get; set; }

    [Column(Order = 4)]
    public string LastName { get; set; }

    #region Related data

    public ICollection<CommentEntity> Comments { get; set; }

    public ICollection<FriendEntity> InvitedByUsers { get; set; }

    public ICollection<FriendEntity> InvitedUsers { get; set; }

    public ICollection<MessageEntity> Messages { get; set; }

    public ICollection<PostEntity> Posts { get; set; }

    public RefreshTokenEntity RefreshToken { get; set; }

    #endregion Related data
}