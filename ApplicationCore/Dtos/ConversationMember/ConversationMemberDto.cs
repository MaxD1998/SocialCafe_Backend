using ApplicationCore.Dtos.User;

namespace ApplicationCore.Dtos.ConversationMember;

public class ConversationMemberDto : ConversationMemberInputDto
{
    public int Id { get; set; }

    public UserDto User { get; set; }
}