using Microsoft.Recognizers.Text;
using System.Text;
using TR.Engine.Contract;
using TR.Engine.Model;
using TR.Engine.Utilities;

namespace TR.Engine.Services
{
    public class PdfParserService : IPdfParserService
    {
        public Dictionary<string, IEnumerable<ModelResult>> Execute(List<string> input, int randomAmount, string culture, ScopeDetectionStrategyEnum strategyName)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var narrowed = ScopeDetectionStrategy.ResolveStrategy(strategyName, input, randomAmount);

            var dic = new Dictionary<string, IEnumerable<ModelResult>>();
            foreach (var item in narrowed)
            {
                var results = PdfParserUtility.ParseAll(item, culture);
                dic.TryAdd(item, results);
            }

            return dic;
        }
    }
}
