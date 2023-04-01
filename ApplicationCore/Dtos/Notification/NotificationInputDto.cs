using Domain.Enums;

namespace ApplicationCore.Dtos.Notification;

public class NotificationInputDto
{
    public bool IsRead { get; set; }

    public Guid RecipientId { get; set; }

    public NotificationType Type { get; set; }

    public Guid UserId { get; set; }
}