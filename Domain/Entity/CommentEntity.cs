using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("Comment")]
    public class CommentEntity : BaseEntity
    {
        [Column(Order = 1)]
        public int PostId { get; set; }

        public string Text { get; set; }

        [Column(Order = 2)]
        public int? UserId { get; set; }

        #region Related data

        public PostEntity Post { get; set; }

        public UserEntity User { get; set; }

        #endregion Related data
    }
}