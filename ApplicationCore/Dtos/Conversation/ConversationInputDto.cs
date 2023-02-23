using ApplicationCore.Dtos.ConversationMember;

namespace ApplicationCore.Dtos.Conversation;

public class ConversationInputDto : ConversationBaseDto
{
    public List<ConversationMemberInputDto> ConversationMembers { get; set; }
}