using Microsoft.Recognizers.Text;
using TR.Engine;

namespace ML.Engine.Contract
{
    public interface IExtractNumbersService
    {
        Dictionary<string, IEnumerable<ModelResult>> Execute(ExtractNumbersModelWorkflowClass input);
    }
}