namespace ApplicationCore.Extensions
{
    public static class ObjectExtension
    {
        public static void ThrowIfNull(this object source, Exception exception)
        {
            if (source is null)
            {
                throw exception;
            }
        }

        public static void ThrowIfNullOrEmpty(this IEnumerable<object> sources, Exception exception)
        {
            if (sources is null || sources.Count() == 0)
            {
                throw exception;
            }
        }
    }
}