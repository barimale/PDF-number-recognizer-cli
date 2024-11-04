using DetectLanguage;
using System.Collections.Concurrent;
using TR.Engine.Contract;
using TR.Engine.Model;
using TR.Engine.Utilities;

namespace TR.Engine.Services
{
    public class LanguageDetector : ILanguageDetector
    {
        public async Task<string?> Detect(List<string> input, int randomAmount)
        {
            DetectLanguageClient client = new DetectLanguageClient("30794da0c353725a323396aebfa864c2");

            var narrowed = ScopeDetectionStrategy.ResolveStrategy(ScopeDetectionStrategyEnum.ExecuteRandom, input, randomAmount);

            var results = new ConcurrentBag<string>();
            narrowed.ForEach( p => results.Add(
                 client.DetectCodeAsync(p).Result)
            );

#if DEBUG
            var theMostFrequents = results
                .ToList()
                .Where(p => !string.IsNullOrEmpty(p))
                .GroupBy(p => p)
                .OrderByDescending(pp => pp.Count());
#endif
            var theMostFrequent = results
                .ToList()
                .Where(p => !string.IsNullOrEmpty(p))
                .GroupBy(p => p)
                .OrderByDescending(pp => pp.Count())
                .FirstOrDefault();

            return theMostFrequent != null ? theMostFrequent.Key : null;
        }
    }
}
