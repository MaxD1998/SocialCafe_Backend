using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("Comment")]
    public class CommentEntity : BaseEntity
    {
        public PostEntity Post { get; set; }

        public int PostId { get; set; }

        public string Text { get; set; }

        public UserEntity User { get; set; }

        public int? UserId { get; set; }
    }
}