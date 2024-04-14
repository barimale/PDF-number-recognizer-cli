using TR.Engine.Contract;

namespace TR.Engine
{
    public class DetectLanguageWorkflow : IDetectLanguageWorkflow
    {
        private readonly IPdfReaderService _pdfService;
        private readonly ILanguageDetector _languageDetector;

        private DetectLanguageWorkflowClass _input;

        public DetectLanguageWorkflow(
            IPdfReaderService pdfService,
            ILanguageDetector languageDetector)
        {
            _pdfService = pdfService;
            _languageDetector = languageDetector;
        }

        public string? Execute(DetectLanguageWorkflowClass input)
        {
            try
            {
                _input = input;

                var output = _pdfService.ExtractTextFromPDF(_input.PdfPath);
                var result = _languageDetector.Detect(
                    output, 
                    _input.RandomAmount,
                    _input.Strategy).Result;

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
        public string Strategy { get; set; }
    }
}
