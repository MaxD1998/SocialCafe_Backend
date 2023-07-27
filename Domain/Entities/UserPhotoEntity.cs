using Domain.Bases;

namespace Domain.Entities;

public class UserPhotoEntity : BaseFileEntity
{
    public bool IsMain { get; set; }

    public Guid UserId { get; set; }

    #region Related data

    public UserEntity User { get; set; }

    #endregion Related data
}