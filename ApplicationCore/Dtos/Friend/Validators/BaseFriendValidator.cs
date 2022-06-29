using ApplicationCore.Interfaces.Repositories;
using Domain.Entity;
using FluentValidation;

namespace ApplicationCore.Dtos.Friend.Validators
{
    public class BaseFriendValidator<T> : AbstractValidator<T> where T : FriendInputDto
    {
        public BaseFriendValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.InviterId)
                .NotEmpty();
            RuleFor(x => x.RecipientId)
                .NotEmpty();

            RuleFor(x => new { x.InviterId, x.RecipientId })
                .Custom((value, context) =>
                {
                    var isFriendExist = unitOfWork.BaseRepository
                        .CheckRecordExist<FriendEntity>(x => (x.InviterId.Equals(value.InviterId) && x.RecipientId.Equals(value.RecipientId))
                            || x.InviterId.Equals(value.RecipientId) && x.RecipientId.Equals(value.RecipientId));

                    isFriendExist.Wait();

                    if (isFriendExist.Result)
                        context.AddFailure(nameof(value), "Users are already friends");
                });
        }
    }
}