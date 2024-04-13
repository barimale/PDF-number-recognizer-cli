using DetectLanguage;

namespace TR.Engine.Services
{
    public class LanguageDetector
    {
        public async Task<string?> Detect(List<string> input, int randomAmount)
        {
            // WIP change this
            DetectLanguageClient client = new DetectLanguageClient("30794da0c353725a323396aebfa864c2");

            var rnd = new Random();
            var narrowed = input.OrderBy(x => rnd.Next()).Take(randomAmount).ToList();

            var results = new List<string>();
            narrowed.ForEach(async p => results.Add(
                client.DetectCodeAsync(p).Result)
            );

            var theMostFrequent = results
                .Where(p => !string.IsNullOrEmpty(p))
                .GroupBy(p => p)
                .OrderByDescending(pp => pp.Key)
                .FirstOrDefault();

            return theMostFrequent != null ? theMostFrequent.Key : null;
        }
    }
}
