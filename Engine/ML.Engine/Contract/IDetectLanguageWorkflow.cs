using TR.Engine;

namespace ML.Engine.Contract
{
    public interface IDetectLanguageWorkflow
    {
        string? Execute(DetectLanguageWorkflowClass input);
        int? PdfLineCount(DetectLanguageWorkflowClass input);
    }
}