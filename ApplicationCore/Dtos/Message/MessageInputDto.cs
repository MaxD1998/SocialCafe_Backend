namespace ApplicationCore.Dtos.Message;

public class MessageInputDto
{
    public int ConversationId { get; set; }

    public string Text { get; set; }

    public int UserId { get; set; }
}