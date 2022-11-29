using Api.Bases;
using ApplicationCore.Cqrs.Post.Create;
using ApplicationCore.Cqrs.Post.Get;
using ApplicationCore.Dtos.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class PostController : BaseApiController
    {
        public PostController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePostAsync([FromBody] PostInputDto dto)
        {
            var result = await _mediator.Send(new CreatePostCommand(dto));

            return Ok(result);
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<PostDto>> GetPostByIdAsync([FromRoute] int postId)
        {
            var result = await _mediator.Send(new GetPostByIdQuery(postId));

            return Ok(result);
        }

        [HttpGet("UserId/{userId}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsByUserIdAsync([FromRoute] int userId)
        {
            var result = await _mediator.Send(new GetPostsByUserIdQuery(userId));

            return Ok(result);
        }
    }
}