namespace Common.Extensions
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
    }
}