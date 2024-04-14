namespace TR.Engine.Utilities
{
    public class LanguageDetectionStrategy
    {
        public static List<string>? ExecuteRandom(List<string> input, int randomAmount)
        {
            var rnd = new Random();
            var narrowed = input.OrderBy(x => rnd.Next()).Take(randomAmount).ToList();

            return narrowed;
        }

        public static List<string>? ExecuteAll(List<string> input)
        {
            var rnd = new Random();
            var narrowed = input.OrderBy(x => rnd.Next()).Take(input.Count).ToList();

            return narrowed;
        }
    }
}
