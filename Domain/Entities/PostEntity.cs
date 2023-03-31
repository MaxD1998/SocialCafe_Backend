using Domain.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PostEntity : BaseEntity
{
    public string Text { get; set; }

    [Column(Order = 3)]
    public Guid UserId { get; set; }

    #region Related data

    public ICollection<CommentEntity> Comments { get; set; }

    public UserEntity User { get; set; }

    #endregion Related data
}