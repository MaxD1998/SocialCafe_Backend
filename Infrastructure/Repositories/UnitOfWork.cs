using ApplicationCore.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBaseRepository BaseRepository => new BaseRepository();

        public IRefreshTokenRepository RefreshTokenRepository => new RefreshTokenRepository();
    }
}