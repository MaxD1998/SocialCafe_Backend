using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class ConversationMemberEntity : BaseEntity
    {
        [Column(Order = 1)]
        public int ConversationId { get; set; }

        public string Nick { get; set; }

        [Column(Order = 2)]
        public int UserId { get; set; }

        #region Related data

        public UserEntity User { get; set; }

        #endregion Related data
    }
}