using ApplicationCore.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBaseRepository BaseRepository => new BaseRepository();

        public IUserRepository UserRepository => new UserRepository();
    }
}