namespace ApplicationCore.Interfaces;

public interface IChatService
{
    Task<IEnumerable<string>> GetConnectionIds(Guid userId);

    Task UpdateUserConnectionId(Guid userId, string connectionId);
}