using ApplicationCore.Dtos.User;

namespace ApplicationCore.Dtos.Friend
{
    public class FriendDto
    {
        public UserDto Friend { get; set; }

        public int FriendId { get; set; }

        public int Id { get; set; }
    }
}