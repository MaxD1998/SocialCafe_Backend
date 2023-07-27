using Api.Bases;
using ApplicationCore.Cqrs.UserPhoto.Get;
using ApplicationCore.Dtos.UserPhoto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[AllowAnonymous]
public class UserPhotoController : BaseApiController
{
    public UserPhotoController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("Main/{userId}")]
    public async Task<ActionResult<FileContentResult>> GetMain([FromRoute] Guid userId)
        => await ApiFileResponseAsync<GetMainUserPhotoByUserIdQuery>(new(userId));

    [HttpGet("Mains")]
    public async Task<ActionResult<IEnumerable<UserPhotoListDto>>> GetMains([FromQuery] IEnumerable<Guid> userIds)
        => await ApiFilesResponseAsync<UserPhotoListDto, GetMainUserPhotosByUserIdQuery>(new(userIds));

    [HttpPost]
    public IActionResult Test([FromForm] IFormFile image)
    {
        var file = GetBytes(image);
        return File(file, image.ContentType);
    }

    private byte[] GetBytes(IFormFile image)
    {
        using (var memory = new MemoryStream())
        {
            image.CopyTo(memory);
            var file = memory.ToArray();

            return file;
        }
    }
}

public class FileModel
{
    public IFormFile Image { get; set; }

    public string Name { get; set; }
}