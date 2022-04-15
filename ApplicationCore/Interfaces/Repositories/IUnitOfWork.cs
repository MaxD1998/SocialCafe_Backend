namespace ApplicationCore.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public IBaseRepository BaseRepository { get; }

        public IRefreshTokenRepository RefreshTokenRepository { get; }
    }
}