using FluentValidation;

namespace ApplicationCore.Dtos.Post.Validators;

public class BasePostValidator<T> : AbstractValidator<T> where T : PostInputDto
{
    public BasePostValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty();
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}