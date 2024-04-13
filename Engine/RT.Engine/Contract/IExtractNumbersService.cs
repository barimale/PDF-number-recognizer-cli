using Microsoft.Recognizers.Text;

namespace TR.Engine.Contract
{
    public interface IExtractNumbersService
    {
        Dictionary<string, IEnumerable<ModelResult>> Execute(ExtractNumbersModelWorkflowClass input);
    }
}