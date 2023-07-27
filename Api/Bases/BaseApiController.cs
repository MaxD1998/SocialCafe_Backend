using ApplicationCore.Bases;
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

    protected async Task<ActionResult<FileContentResult>> ApiFileResponseAsync<TRequest>(TRequest request) where TRequest : notnull, IRequest<BaseFileDto>
    {
        var result = await _mediator.Send(request);
        return result != null
            ? File(result.Data, result.ContentType, result.Name)
            : NoContent();
    }

    protected async Task<ActionResult<IEnumerable<TResult>>> ApiFilesResponseAsync<TResult, TRequest>(TRequest request) where TRequest : notnull, IRequest<IEnumerable<BaseFileListDto>>
    {
        var results = await _mediator.Send(request);

        foreach (var result in results)
            result.DataContent = Convert.ToBase64String(result.Data);

        return Ok(results);
    }

    protected async Task<ActionResult<TResult>> ApiResponseAsync<TResult, TRequest>(TRequest request) where TRequest : notnull, IBaseRequest
        => Ok(await _mediator.Send(request));
}