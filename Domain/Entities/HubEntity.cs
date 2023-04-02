using Domain.Bases;
using Domain.Enums;

namespace Domain.Entities;

public class HubEntity : BaseEntity
{
    public string ConnectionId { get; set; }

    public HubType Type { get; set; }

    public Guid UserId { get; set; }

    #region Related data

    public UserEntity User { get; set; }

    #endregion Related data
}