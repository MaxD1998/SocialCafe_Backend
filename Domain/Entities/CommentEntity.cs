using Domain.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class CommentEntity : BaseEntity
{
    [Column(Order = 3)]
    public Guid PostId { get; set; }

    public string Text { get; set; }

    [Column(Order = 4)]
    public Guid? UserId { get; set; }

    #region Related data

    public PostEntity Post { get; set; }

    public UserEntity User { get; set; }

    #endregion Related data
}