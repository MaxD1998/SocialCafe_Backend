using ApplicationCore.Dtos.Comment;
using ApplicationCore.Dtos.User;

namespace ApplicationCore.Dtos.Post;

public class PostDto : PostInputDto
{
    public List<CommentDto> Comments { get; set; }

    public Guid Id { get; set; }

    public UserDto User { get; set; }
}