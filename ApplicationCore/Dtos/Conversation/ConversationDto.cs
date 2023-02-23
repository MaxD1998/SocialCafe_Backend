using ApplicationCore.Dtos.ConversationMember;
using ApplicationCore.Dtos.Message;

namespace ApplicationCore.Dtos.Conversation;

public class ConversationDto : ConversationBaseDto
{
    public List<ConversationMemberDto> ConversationMembers { get; set; }

    public int Id { get; set; }

    public MessageDto Message { get; set; }
}