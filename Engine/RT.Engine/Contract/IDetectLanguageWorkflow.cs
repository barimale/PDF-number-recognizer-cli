namespace TR.Engine.Contract
{
    public interface IDetectLanguageWorkflow
    {
        string? Execute(DetectLanguageWorkflowClass input);
    }
}