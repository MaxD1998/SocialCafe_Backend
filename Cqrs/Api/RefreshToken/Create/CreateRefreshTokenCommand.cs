using Domain.Entity;
using MediatR;

namespace Cqrs.Api.RefreshToken.Create
{
    public record CreateRefreshTokenCommand(int UserId, RefreshTokenEntity Entity) : IRequest<RefreshTokenEntity>;
}