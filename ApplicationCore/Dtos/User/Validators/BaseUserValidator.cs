using FluentValidation;

namespace ApplicationCore.Dtos.User.Validators
{
    public class BaseUserValidator<T> : AbstractValidator<T> where T : UserInputDto
    {
        public BaseUserValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .Length(10, 50);
            RuleFor(x => x.FirstName)
                .Length(1, 50)
                .NotEmpty();
            RuleFor(x => x.LastName)
                .Length(1, 50)
                .NotEmpty();
        }
    }
}