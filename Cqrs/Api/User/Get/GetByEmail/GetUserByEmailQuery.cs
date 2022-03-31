using Domain.Entity;
using MediatR;

namespace Cqrs.Api.User.Get.GetByEmail
{
    public record GetUserByEmailQuery(string Email) : IRequest<UserEntity>;
}