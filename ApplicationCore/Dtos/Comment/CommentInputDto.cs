namespace ApplicationCore.Dtos.Comment
{
    public class CommentInputDto
    {
        public int? ParentId { get; set; }

        public int PostId { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }
    }
}