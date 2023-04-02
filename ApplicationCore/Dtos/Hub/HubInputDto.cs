using Domain.Enums;

namespace ApplicationCore.Dtos.Hub;

public class HubInputDto
{
    public string ConnectionId { get; set; }

    public HubType Type { get; set; }

    public Guid UserId { get; set; }
}