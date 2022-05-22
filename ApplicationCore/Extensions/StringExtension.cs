namespace ApplicationCore.Extensions
{
    public static class StringExtension
    {
        public static string ToFirstUpper(this string text)
        {
            if (text is null || text == string.Empty)
            {
                return text;
            }

            var firstLetter = text.First()
                .ToString()
                .ToUpper();

            text.Replace(text.First(), char.Parse(firstLetter));

            return text;
        }
    }
}