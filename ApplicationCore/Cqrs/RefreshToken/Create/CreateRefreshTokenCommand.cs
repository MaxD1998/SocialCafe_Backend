using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.RefreshToken.Create
{
    public record CreateRefreshTokenCommand(int UserId, RefreshTokenEntity Entity) : IRequest<RefreshTokenEntity>;
}