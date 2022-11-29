using Domain.Base;

namespace Domain.Entity
{
    public class ConversationEntity : BaseEntity
    {
        public string Name { get; set; }

        #region Related data

        public ICollection<ConversationMemberEntity> ConversationMembers { get; set; }

        public ICollection<MessageEntity> Messages { get; set; }

        #endregion Related data
    }
}