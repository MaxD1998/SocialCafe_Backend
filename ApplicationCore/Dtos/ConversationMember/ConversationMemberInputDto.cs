namespace ApplicationCore.Dtos.ConversationMember;

public class ConversationMemberInputDto
{
    public int ConversationId { get; set; }

    public string Nick { get; set; }

    public int UserId { get; set; }
}