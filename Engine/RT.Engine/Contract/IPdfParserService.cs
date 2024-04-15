using Microsoft.Recognizers.Text;
using TR.Engine.Model;

namespace TR.Engine.Contract
{
    public interface IPdfParserService
    {
        Dictionary<string, IEnumerable<ModelResult>> Execute(List<string> output, int randomAmount, string culture, ScopeDetectionStrategyEnum strategyName);
    }
}