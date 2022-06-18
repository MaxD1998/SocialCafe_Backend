using ApplicationCore.Dtos.User;

namespace ApplicationCore.Dtos.Comment
{
    public class CommentDto : CommentInputDto
    {
        public List<CommentDto> Childrens { get; set; }

        public int Id { get; set; }

        public CommentDto Parent { get; set; }

        public UserDto User { get; set; }
    }
}