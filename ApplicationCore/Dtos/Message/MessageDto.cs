using ApplicationCore.Dtos.Conversation;
using ApplicationCore.Dtos.User;

namespace ApplicationCore.Dtos.Message
{
    public class MessageDto : MessageInputDto
    {
        public ConversationDto Conversation { get; set; }

        public int Id { get; set; }

        public UserDto User { get; set; }
    }
}