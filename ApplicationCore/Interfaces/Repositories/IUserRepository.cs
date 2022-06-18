using Domain.Entity;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> CreateRefreshTokenAsync(int userId, RefreshTokenEntity entity);

        Task<bool> DeleteRefreshTokensAsync(int userId, IEnumerable<RefreshTokenEntity> entities);
    }
}