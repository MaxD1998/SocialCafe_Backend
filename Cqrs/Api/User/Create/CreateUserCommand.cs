using Domain.Entity;
using MediatR;

namespace Cqrs.Api.User.Create
{
    public record CreateUserCommand(UserEntity Entity) : IRequest<UserEntity>;
}