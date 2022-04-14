using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.RefreshToken.Update
{
    public record UpdateRefreshTokenCommand(int UserId, RefreshTokenEntity Entity) : IRequest<RefreshTokenEntity>;
}