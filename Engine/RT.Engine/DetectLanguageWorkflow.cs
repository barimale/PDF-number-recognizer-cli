using TR.Engine.Contract;
using TR.Engine.Services;

namespace TR.Engine
{
    public class DetectLanguageWorkflow : IDetectLanguageWorkflow
    {
        private DetectLanguageWorkflowClass _input;
        public string? Execute(DetectLanguageWorkflowClass input)
        {
            try
            {
                _input = input;

                var engine = new PdfReaderService();
                var output = engine.ExtractTextFromPDF(_input.PdfPath);

                var detector = new LanguageDetector();
                var result = detector.Detect(output, _input.RandomAmount).Result;

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class DetectLanguageWorkflowClass
    {
        public int RandomAmount { get; set; }
        public string PdfPath { get; set; }
    }
}
