using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Base
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : ControllerBase
    {
    }
}