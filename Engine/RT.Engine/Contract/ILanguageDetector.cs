namespace TR.Engine.Contract
{
    public interface ILanguageDetector
    {
        Task<string?> Detect(List<string> input, int randomAmount, string strategyName);
    }
}