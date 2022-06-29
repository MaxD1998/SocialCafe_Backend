using ApplicationCore.Dtos.User;

namespace ApplicationCore.Dtos.Comment
{
    public class CommentDto : CommentInputDto
    {
        public int Id { get; set; }

        public UserDto User { get; set; }
    }
}