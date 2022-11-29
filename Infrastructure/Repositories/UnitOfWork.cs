using ApplicationCore.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBaseRepository BaseRepository => new BaseRepository();
    }
}