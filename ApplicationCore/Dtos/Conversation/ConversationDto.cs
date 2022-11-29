using ApplicationCore.Dtos.Message;

namespace ApplicationCore.Dtos.Conversation
{
    public class ConversationDto : ConversationInputDto
    {
        public int Id { get; set; }

        public List<MessageDto> Messages { get; set; }
    }
}