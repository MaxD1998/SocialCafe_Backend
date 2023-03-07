using ApplicationCore.Dtos.User;

namespace ApplicationCore.Dtos.Friend;

public class FriendDto
{
    public Guid Id { get; set; }

    public UserDto User { get; set; }

    public Guid UserId { get; set; }
}