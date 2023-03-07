namespace ApplicationCore.Dtos.Message;

public class MessageInputDto
{
    public Guid ConversationId { get; set; }

    public string Text { get; set; }

    public Guid UserId { get; set; }
}