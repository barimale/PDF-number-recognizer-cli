using Microsoft.Recognizers.Text;
using TR.Engine.Contract;
using TR.Engine.Services;

namespace TR.Engine
{
    public class ExtractNumbersWorkflow : IExtractNumbersService
    {
        private ExtractNumbersModelWorkflowClass _input;

        public Dictionary<string, IEnumerable<ModelResult>> Execute(ExtractNumbersModelWorkflowClass input)
        {
            _input = input;

            var engine = new PdfReaderService();
            var output = engine.ExtractTextFromPDF(_input.PdfPath);

            if (_input.Culture == null)
            {
                var detector = new LanguageDetector();
                var result = detector.Detect(output, 20).Result;
                _input.Culture = result;
            }

            var parser = new PdfParserService();
            var results = parser.Execute(
                output,
                _input.RandomAmount,
                _input.Culture);

            return results;
        }
    }

    public class ExtractNumbersModelWorkflowClass
    {
        public string PdfPath { get; set; }
        public string? Culture { get; set; }
        public int RandomAmount { get; set; }
    }
}
