using ML.Engine.Services;

namespace ML.Engine.Contract
{
    public interface IDetectLanguageWorkflow
    {
        string? Execute(DetectLanguageWorkflowClass input);
    }
}