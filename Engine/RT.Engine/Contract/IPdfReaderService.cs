namespace TR.Engine.Contract
{
    public interface IPdfReaderService
    {
        List<string> ExtractTextFromPDF(string filePath);
    }
}