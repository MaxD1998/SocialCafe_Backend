using ApplicationCore.Dtos.User;

namespace ApplicationCore.Dtos.Friend;

public class FriendDto
{
    public UserDto User { get; set; }

    public int UserId { get; set; }

    public int Id { get; set; }
}