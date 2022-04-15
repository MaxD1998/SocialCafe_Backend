using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces.Repositories;
using Common.Constants;
using Common.Extensions;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public async Task<RefreshTokenEntity> CreateAsync(int userId, RefreshTokenEntity entity)
        {
            using (var context = new DataContext())
            {
                var user = await context.Users
                    .FirstOrDefaultAsync(x => x.Id.Equals(userId));

                user.RefreshTokens.Add(entity);
                await context.SaveChangesAsync();

                return user.RefreshTokens
                    .LastOrDefault();
            }
        }

        public async Task<RefreshTokenEntity> UpdateAsync(int userId, RefreshTokenEntity entity)
        {
            using (var context = new DataContext())
            {
                var user = await context.Users
                    .FirstOrDefaultAsync(x => x.Id.Equals(userId));

                user.ThrowIfNull(new NotFoundException(ExceptionMessageConst.RefreshTokenUpdateFailed));

                var refreshToken = user.RefreshTokens
                    .FirstOrDefault(x => x.RemoteAddress.Equals(entity.RemoteAddress));

                refreshToken.ThrowIfNull(new NotFoundException(ExceptionMessageConst.RefreshTokenUpdateFailed));

                Map(entity, refreshToken);
                await context.SaveChangesAsync();

                return refreshToken;
            }
        }

        private void Map(RefreshTokenEntity source, RefreshTokenEntity destination)
        {
            destination.CreationDate = source.CreationDate;
            destination.ExpireDate = source.ExpireDate;
            destination.Token = source.Token;
        }
    }
}