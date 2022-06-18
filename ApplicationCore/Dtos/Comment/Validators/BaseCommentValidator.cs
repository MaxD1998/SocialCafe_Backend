using FluentValidation;

namespace ApplicationCore.Dtos.Comment.Validators
{
    public class BaseCommentValidator<T> : AbstractValidator<T> where T : CommentInputDto
    {
        public BaseCommentValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();
            RuleFor(x => x.PostId)
                .NotEmpty();
        }
    }
}