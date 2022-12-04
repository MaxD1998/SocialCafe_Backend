using ApplicationCore.Dtos.ConversationMember;

namespace ApplicationCore.Dtos.Conversation
{
    public class ConversationDto : ConversationInputDto
    {
        public List<ConversationMemberDto> ConversationMembers { get; set; }

        public int Id { get; set; }
    }
}