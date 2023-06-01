using Domain.Entities;

namespace ApplicationCore.Dtos.Friend;

public class FriendEntityDto : FriendEntity
{
    public Guid UserId { get; set; }
}