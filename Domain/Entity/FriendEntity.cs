using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("Friend")]
    public class FriendEntity : BaseEntity
    {
        public int InviterId { get; set; }

        public int RecipientId { get; set; }

        #region Related data

        public UserEntity Inviter { get; set; }

        public UserEntity Recipient { get; set; }

        #endregion Related data
    }
}