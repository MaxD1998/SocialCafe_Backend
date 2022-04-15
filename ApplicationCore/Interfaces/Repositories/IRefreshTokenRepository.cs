using Domain.Entity;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenEntity> CreateAsync(int userId, RefreshTokenEntity entity);

        Task<RefreshTokenEntity> UpdateAsync(int userId, RefreshTokenEntity entity);
    }
}