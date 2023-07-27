using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Bases;

public class BaseFileEntity : BaseEntity
{
    public string ContentType { get; set; }

    public byte[] Data { get; set; }

    public string Description { get; set; }

    public bool IsPublic { get; set; }

    [Column(Order = 3)]
    public string Name { get; set; }
}