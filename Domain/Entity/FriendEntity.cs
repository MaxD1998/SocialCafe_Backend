using Domain.Base;

namespace Domain.Entity
{
    public class FriendEntity : BaseEntity
    {
        public UserEntity Inviter { get; set; }

        public int InviterId { get; set; }

        public UserEntity Recipient { get; set; }

        public int RecipientId { get; set; }
    }
}