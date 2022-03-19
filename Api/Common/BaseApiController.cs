using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Common
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}