namespace TR.Engine.Utilities
{
    // WIP extract parameter in subcommands
    // Activator <- use it, parameter has only three params: ExecuteRandom...
    public class LanguageDetectionStrategy
    {
        public static List<string>? ExecuteRandom(List<string> input, int randomAmount)
        {
            var rnd = new Random();
            var narrowed = input.OrderBy(x => rnd.Next()).Take(randomAmount).ToList();

            return narrowed;
        }

        public static List<string>? ExecuteTop1000OrAll(List<string> input)
        {
            var randomAmount = Math.Min(input.Count, 1000);

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
