using DataAccess;
using Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cqrs.Api.RefreshToken.Create
{
    public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, RefreshTokenEntity>
    {
        public async Task<RefreshTokenEntity> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            using (var context = new DataContext())
            {
                var user = await context.Users
                    .FirstOrDefaultAsync(x => x.Id.Equals(request.UserId));

                user.RefreshTokens.Add(request.Entity);
                await context.SaveChangesAsync();

                return user.RefreshTokens
                    .LastOrDefault();
            }
        }
    }
}