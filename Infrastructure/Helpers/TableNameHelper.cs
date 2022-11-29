namespace Infrastructure.Helpers
{
    public static class TableNameHelper
    {
        public static string Convert(string entityTable)
            => entityTable.Replace("Entity", string.Empty);
    }
}