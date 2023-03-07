namespace ApplicationCore.Dtos.ConversationMember;

public class ConversationMemberInputDto
{
    public Guid ConversationId { get; set; }

    public string Nick { get; set; }

    public Guid UserId { get; set; }
}