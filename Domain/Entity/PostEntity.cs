using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("Post")]
    public class PostEntity : BaseEntity
    {
        public List<CommentEntity> Comments { get; set; }

        public string Text { get; set; }

        public UserEntity User { get; set; }

        public int UserId { get; set; }
    }
}