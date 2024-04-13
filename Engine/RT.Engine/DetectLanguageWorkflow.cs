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
                var output = engine.ExtractTextFromPDF(_input.OutputModelPath);

                var detector = new LanguageDetector();
                var result = detector.Detect(output, _input.RandomAmount).Result;

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int? PdfLineCount(DetectLanguageWorkflowClass input)
        {
            try
            {
                _input = input;

                var engine = new PdfReaderService();
                var output = engine.ExtractTextFromPDF(_input.OutputModelPath);

                return output.Count;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public class DetectLanguageWorkflowClass
    {
        public int RandomAmount { get; set; }
        public string OutputModelPath { get; set; }
    }
}
