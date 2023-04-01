using ApplicationCore.Dtos.User;

namespace ApplicationCore.Dtos.Notification;

public class NotificationDto : NotificationInputDto
{
    public Guid Id { get; set; }

    public UserDto User { get; set; }
}