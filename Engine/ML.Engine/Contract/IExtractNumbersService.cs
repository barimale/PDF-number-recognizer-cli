using TR.Engine;

namespace ML.Engine.Contract
{
    public interface IExtractNumbersService
    {
        bool Execute(ExtractNumbersModelWorkflowClass input);
    }
}