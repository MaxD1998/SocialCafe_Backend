using ApplicationCore.Interfaces.Repositories;

namespace ApplicationCore.Dtos.Friend.Validators;

public class FriendValidator : BaseFriendValidator<FriendInputDto>
{
    public FriendValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}