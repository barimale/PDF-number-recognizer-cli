using Microsoft.Recognizers.Text;
using TR.Engine;

namespace TR.Engine.Contract
{
    public interface IExtractNumbersService
    {
        Dictionary<string, IEnumerable<ModelResult>> Execute(ExtractNumbersModelWorkflowClass input);
    }
}