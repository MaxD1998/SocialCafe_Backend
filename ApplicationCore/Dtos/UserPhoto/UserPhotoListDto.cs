using ApplicationCore.Bases;

namespace ApplicationCore.Dtos.UserPhoto;

public class UserPhotoListDto : BaseFileListDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
}