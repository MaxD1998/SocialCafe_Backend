using Api.Bases;
using ApplicationCore.Cqrs.Comment.Create;
using ApplicationCore.Cqrs.Comment.Get;
using ApplicationCore.Dtos.Comment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class CommentController : BaseApiController
    {
        public CommentController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<CommentDto>> CreateCommentAsync([FromBody] CommentInputDto dto)
        {
            var result = await _mediator.Send(new CreateCommentCommand(dto));

            return Ok(result);
        }

        [HttpGet("PostId/{postId}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsAsync([FromRoute] int postId)
        {
            var result = await _mediator.Send(new GetCommentsByPostIdQuery(postId));

            return Ok(result);
        }
    }
}