using FluentValidation;

namespace ApplicationCore.Dtos.RefreshToken;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenInputDto>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.CreationDate)
            .NotEmpty();
        RuleFor(x => x.ExpireDate)
            .NotEmpty();
        RuleFor(x => x.Token)
            .NotEmpty();
    }
}