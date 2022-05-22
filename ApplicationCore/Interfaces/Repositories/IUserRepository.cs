using Domain.Entity;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> CreateRefreshTokenAsync(int userId, RefreshTokenEntity entity);

        Task<UserEntity> UpdateRefreshTokenAsync(int userId, int refreshTokenId, RefreshTokenEntity entity);
    }
}