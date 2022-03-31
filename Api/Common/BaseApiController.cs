using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Common
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}