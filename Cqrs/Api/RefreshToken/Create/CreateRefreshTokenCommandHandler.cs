using Domain.Entity;
using MediatR;

namespace Cqrs.Api.RefreshToken.Create
{
    public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, RefreshTokenEntity>
    {
        public async Task<RefreshTokenEntity> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}