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

    [HttpGet("{postId}")]
    public async Task<ActionResult<PostDto>> GetByIdAsync([FromRoute] int postId)
        => await ApiResponseAsync<PostDto, GetPostByIdQuery>(new(postId));

    [HttpGet("UserId/{userId}")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetsByUserIdAsync([FromRoute] int userId)
        => await ApiResponseAsync<IEnumerable<PostDto>, GetPostsByUserIdQuery>(new(userId));
}