using ApplicationCore.Interfaces.Repositories;
using Domain.Entity;
using FluentValidation;

namespace ApplicationCore.Dtos.User.Validators;

public class RegisterValidator : BaseUserValidator<RegisterDto>
{
    public RegisterValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Email)
            .Custom((value, context) =>
            {
                var isUserExist = unitOfWork.BaseRepository
                    .CheckRecordExist<UserEntity>(x => x.Email.ToLower() == value.ToLower());

                isUserExist.Wait();

                if (isUserExist.Result)
                    context.AddFailure(nameof(value), "That email is taken");
            });

        RuleFor(x => x.Password)
            .Length(5, 25)
            .NotEmpty();
    }
}