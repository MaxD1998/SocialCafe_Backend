using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Base;

public abstract class BaseEntity
{
    [Column(Order = 0)]
    [Key]
    public int Id { get; private init; }
}