using FluentValidation;

namespace Api.Models.Validators
{
    public class LoginDtoValidator : AbstractValidator<RegisterDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}