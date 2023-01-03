using FluentValidation;

namespace ApplicationCore.Dtos.Login;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}