using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class MessageEntity : BaseEntity
    {
        [Column(Order = 1)]
        public int ConversationId { get; set; }

        [Column(Order = 3)]
        public string Text { get; set; }

        [Column(Order = 2)]
        public int UserId { get; set; }

        #region Related data

        public ConversationEntity Converstaion { get; set; }

        public UserEntity User { get; set; }

        #endregion Related data
    }
}