using ApplicationCore.Enums;

namespace ApplicationCore.Dtos.User;

public class InviteUserDto : UserDto
{
    public Guid? NotificationId { get; set; }

    public InvitationType Type { get; set; }
}