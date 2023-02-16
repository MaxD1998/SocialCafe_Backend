namespace ApplicationCore.Interfaces;

public interface IChatService
{
    Task<IEnumerable<string>> GetConnectionIds(int userId);

    Task UpdateUserConnectionId(int userId, string connectionId);
}