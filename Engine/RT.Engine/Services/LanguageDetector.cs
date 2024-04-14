using DetectLanguage;
using System.Collections.Concurrent;
using TR.Engine.Contract;

namespace TR.Engine.Services
{
    public class LanguageDetector : ILanguageDetector
    {
        public async Task<string?> Detect(List<string> input, int randomAmount)
        {
            DetectLanguageClient client = new DetectLanguageClient("30794da0c353725a323396aebfa864c2");

            var rnd = new Random();
            var narrowed = input.OrderBy(x => rnd.Next()).Take(randomAmount).ToList();

            var results = new ConcurrentBag<string>();
            narrowed.AsParallel().ForAll(async p => results.Add(
                await client.DetectCodeAsync(p))
            );

            var theMostFrequent = results
                .ToList()
                .Where(p => !string.IsNullOrEmpty(p))
                .GroupBy(p => p)
                .OrderByDescending(pp => pp.Key)
                .FirstOrDefault();

            return theMostFrequent != null ? theMostFrequent.Key : null;
        }
    }
}
