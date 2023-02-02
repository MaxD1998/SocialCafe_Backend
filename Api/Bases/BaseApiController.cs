using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Bases;

[ApiController]
[Authorize]
[Route("[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected readonly IMediator _mediator;

    public BaseApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async Task<ActionResult<TResult>> ApiResponseAsync<TResult, TRequest>(TRequest request) where TRequest : notnull, IBaseRequest
    {
        var result = await _mediator.Send(request);

        return Ok(result);
    }
}