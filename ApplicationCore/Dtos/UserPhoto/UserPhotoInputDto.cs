using ApplicationCore.Bases;

namespace ApplicationCore.Dtos.UserPhoto;

public class UserPhotoInputDto : BaseFileDto
{
    public bool IsMain { get; set; }

    public Guid UserId { get; set; }
}