using Domain.Entity;
using MediatR;

namespace Cqrs.Api.RefreshToken.Update
{
    public record UpdateRefreshTokenCommand(int UserId, RefreshTokenEntity Entity) : IRequest<RefreshTokenEntity>;
}