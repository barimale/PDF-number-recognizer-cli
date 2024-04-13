using TR.Engine;

namespace ML.Engine.Contract
{
    public interface ICountLinesWorkflow
    {
        int? Execute(CountLinesWorkflowClass input);
    }
}