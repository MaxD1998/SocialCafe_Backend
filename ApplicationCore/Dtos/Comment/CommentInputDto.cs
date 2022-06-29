namespace ApplicationCore.Dtos.Comment
{
    public class CommentInputDto
    {
        public int PostId { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }
    }
}