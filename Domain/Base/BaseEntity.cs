using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Base
{
    public abstract class BaseEntity
    {
        [Column(Order = 0)]
        public int Id { get; init; }
    }
}