using FluentValidation;

namespace Api.Models.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
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
            RuleFor(x => x.Password)
                .Length(5, 25)
                .NotEmpty();
        }
    }
}