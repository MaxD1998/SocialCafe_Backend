using ApplicationCore.Dtos.ConversationMember;

namespace ApplicationCore.Dtos.Conversation
{
    public class ConversationInputExtendDto : ConversationInputDto
    {
        public List<ConversationMemberInputDto> ConversationMembers { get; set; }
    }
}