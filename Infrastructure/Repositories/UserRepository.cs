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

                return await context.Set<UserEntity>()
                    .FirstOrDefaultAsync(x => x.Id.Equals(userId));
            }
        }

        public async Task<bool> DeleteRefreshTokensAsync(int userId, IEnumerable<RefreshTokenEntity> entities)
        {
            using (var context = new DataContext())
            {
                var user = await context.Set<UserEntity>()
                    .FirstOrDefaultAsync(x => x.Id.Equals(userId));

                user.ThrowIfNull(new NotFoundException(string.Format(ErrorMessages.ValueXWasNull, nameof(user).ToFirstUpper())));

                var removedItemsCount = user.RefreshTokens.RemoveAll(x => entities.Select(x => x.Id).Contains(x.Id));

                await context.SaveChangesAsync();

                return removedItemsCount > 0;
            }
        }
    }
}