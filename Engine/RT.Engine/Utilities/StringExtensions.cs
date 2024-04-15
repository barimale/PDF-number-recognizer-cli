namespace TR.Engine.Utilities
{
    public static class StringExtensions
    {
        public static T ParseEnum<T>(this string value)
            where T : struct
        {
            if(string.IsNullOrEmpty(value))
            {
                return default(T);
            }

            T result;
            return Enum.TryParse<T>(value, out result) ? result : default(T);
        }
    }
}
