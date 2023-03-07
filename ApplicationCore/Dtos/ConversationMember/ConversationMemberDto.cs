using ApplicationCore.Dtos.User;

namespace ApplicationCore.Dtos.ConversationMember;

public class ConversationMemberDto : ConversationMemberInputDto
{
    public Guid Id { get; set; }

    public UserDto User { get; set; }
}