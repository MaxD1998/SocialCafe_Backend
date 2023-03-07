namespace ApplicationCore.Dtos.Friend;

public class FriendInputDto
{
    public Guid InviterId { get; set; }

    public Guid RecipientId { get; set; }
}