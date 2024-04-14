namespace TR.Engine.Utilities
{
    public class ScopeDetectionStrategy
    {
        public static List<string>? ResolveStrategy(string strategyName, List<string> input, int randomAmount)
        {
            switch (strategyName)
            {
                case "ExecuteRandom":
                    return ExecuteRandom(input, randomAmount);
                case "ExecuteTop1000OrAll":
                    return ExecuteTop1000OrAll(input, randomAmount);
                case "ExecuteAll":
                    return ExecuteAll(input, randomAmount);
                default:
                    return ExecuteRandom(input, randomAmount);
            }
        }

        internal static List<string>? ExecuteRandom(List<string> input, int randomAmount)
        {
            var rnd = new Random();
            var narrowed = input.OrderBy(x => rnd.Next()).Take(randomAmount).ToList();

            return narrowed;
        }

        internal static List<string>? ExecuteTop1000OrAll(List<string> input, int randomAmount)
        {
            var randomAmount2= Math.Min(input.Count, 1000);

            var rnd = new Random();
            var narrowed = input.OrderBy(x => rnd.Next()).Take(randomAmount2).ToList();

            return narrowed;
        }

        internal static List<string>? ExecuteAll(List<string> input, int randomAmount)
        {
            var rnd = new Random();
            var narrowed = input.OrderBy(x => rnd.Next()).Take(input.Count).ToList();

            return narrowed;
        }
    }
}
