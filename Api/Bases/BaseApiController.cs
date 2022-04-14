using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Bases
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}