using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Bases;

public abstract class BaseEntity
{
    [Column(Order = 1)]
    public DateTime CreateTime { get; set; }

    [Column(Order = 0)]
    public Guid Id { get; private init; }

    [Column(Order = 2)]
    public DateTime? ModifyTime { get; set; }
}