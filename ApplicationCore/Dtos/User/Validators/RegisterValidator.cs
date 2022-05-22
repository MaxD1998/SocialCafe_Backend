using FluentValidation;

namespace ApplicationCore.Dtos.User.Validators
{
    public class RegisterValidator : BaseUserValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Password)
                .Length(5, 25)
                .NotEmpty();
        }
    }
}