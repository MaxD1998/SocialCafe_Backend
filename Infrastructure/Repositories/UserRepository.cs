using ApplicationCore.Exceptions;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Resources;
using Domain.Entity;
using Infrastructure.Bases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepositoryMapper, IUserRepository
    {
        public async Task<UserEntity> CreateRefreshTokenAsync(int userId, RefreshTokenEntity entity)
        {
            using (var context = new DataContext())
            {
                var user = await context.Set<UserEntity>()
                    .FirstOrDefaultAsync(x => x.Id.Equals(userId));

                user.ThrowIfNull(new NotFoundException(string.Format(ErrorMessages.ValueXWasNull, nameof(user).ToFirstUpper())));

                user.RefreshTokens.Add(entity);
                await context.SaveChangesAsync();

                return user;
            }
        }

        public async Task<UserEntity> UpdateRefreshTokenAsync(int userId, int refereshTokenId, RefreshTokenEntity entity)
        {
            using (var context = new DataContext())
            {
                var user = await context.Set<UserEntity>()
                    .FindAsync(userId);
                user.ThrowIfNull(new NotFoundException(string.Format(ErrorMessages.ValueXWasNull, nameof(user).ToFirstUpper())));

                var refreshToken = user.RefreshTokens
                    .FirstOrDefault(x => x.Id.Equals(refereshTokenId));
                refreshToken.ThrowIfNull(new NotFoundException(string.Format(ErrorMessages.ValueXWasNull, nameof(refreshToken).ToFirstUpper())));

                Map(entity, refreshToken);
                await context.SaveChangesAsync();

                return user;
            }
        }
    }
}