namespace ApplicationCore.Dtos.Comment;

public class CommentInputDto
{
    public Guid PostId { get; set; }

    public string Text { get; set; }

    public Guid UserId { get; set; }
}