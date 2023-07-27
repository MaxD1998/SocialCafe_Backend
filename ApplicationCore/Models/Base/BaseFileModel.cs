using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Models.Base;

public class BaseFileModel
{
    public IFormFile Image { get; set; }
}