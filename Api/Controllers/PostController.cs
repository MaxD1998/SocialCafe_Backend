using Api.Bases;
using ApplicationCore.Cqrs.Post.Create;
using ApplicationCore.Cqrs.Post.Get;
using ApplicationCore.Dtos.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class PostController : BaseApiController
{
    public PostController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> CreateAsync([FromBody] PostInputDto dto)
        => await ApiResponseAsync<PostDto, CreatePostCommand>(new(dto));

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetByIdAsync([FromRoute] Guid id)
        => await ApiResponseAsync<PostDto, GetPostByIdQuery>(new(id));

    [HttpGet("UserAndUserFriends/UserId/{userId}")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetsByUserAndUserFriendsByUserIdAsync([FromRoute] Guid userId)
        => await ApiResponseAsync<IEnumerable<PostDto>, GetPostsByUserAndUserFriendsByUserIdQuery>(new(userId));

    [HttpGet("UserId/{userId}")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetsByUserIdAsync([FromRoute] Guid userId)
        => await ApiResponseAsync<IEnumerable<PostDto>, GetPostsByUserIdQuery>(new(userId));
}