using ML.Engine.Services;

namespace ML.Engine.Contract
{
    public interface IExtractNumbersService
    {
        bool Execute(ExtractNumbersModelWorkflowClass input);
    }
}