using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("User")]
    public class UserEntity : BaseEntity
    {
        public List<CommentEntity> Comments { get; set; }

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

        public List<RefreshTokenEntity> RefreshTokens { get; set; }

        #endregion Related data
    }
}