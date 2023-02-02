using Api.Bases;
using ApplicationCore.Cqrs.Comment.Create;
using ApplicationCore.Cqrs.Comment.Get;
using ApplicationCore.Dtos.Comment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class CommentController : BaseApiController
{
    public CommentController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateAsync([FromBody] CommentInputDto dto)
        => await ApiResponseAsync<CommentDto, CreateCommentCommand>(new(dto));

    [HttpGet("PostId/{postId}")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetsByPostIdAsync([FromRoute] int postId)
        => await ApiResponseAsync<IEnumerable<CommentDto>, GetCommentsByPostIdQuery>(new(postId));
}