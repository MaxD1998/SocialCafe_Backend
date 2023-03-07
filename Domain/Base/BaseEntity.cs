using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Base;

public abstract class BaseEntity
{
    [Column(Order = 1)]
    public DateTime CreateTime { get; set; }

    [Column(Order = 0)]
    [Key]
    public Guid Id { get; private init; }

    [Column(Order = 2)]
    public DateTime? ModifyTime { get; set; }
}