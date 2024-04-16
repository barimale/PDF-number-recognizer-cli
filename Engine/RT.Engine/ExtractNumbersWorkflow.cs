using Elastic.Clients.Elasticsearch;
using Microsoft.Recognizers.Text;
using TR.Engine.Contract;
using TR.Engine.Model;
using TR.Engine.Services;

namespace TR.Engine
{
    public class ExtractNumbersWorkflow : IExtractNumbersService
    {
        private readonly IPdfReaderService _pdfService;
        private readonly ILanguageDetector _languageDetector;
        private readonly IPdfParserService _pdfParserService;

        private ExtractNumbersModelWorkflowClass _input;

        public ExtractNumbersWorkflow(
            IPdfReaderService pdfService,
            ILanguageDetector languageDetector,
            IPdfParserService pdfParserService)
        {
            _pdfService = pdfService;
            _languageDetector = languageDetector;
            _pdfParserService = pdfParserService;
        }

        public Dictionary<string, IEnumerable<ModelResult>> Execute(ExtractNumbersModelWorkflowClass input)
        {
            try
            {
                _input = input;

                var output = _pdfService.ExtractTextFromPDF(_input.PdfPath);

                if (_input.Culture == null)
                {
                    var result = _languageDetector.Detect(output, _input.RandomAmount).Result;
                    _input.Culture = result;
                }

                var results = _pdfParserService.Execute(
                    output,
                    _input.RandomAmount,
                    _input.Culture,
                    _input.Strategy);

                if(_input.ToDb != null)
                {
                    try
                    {
                        var dbEngine = new ToDbService(_input.ToDb);
                        foreach (var result in results)
                        {
                            dbEngine.AddEntity(result, new Id(_input.PdfPath));
                        }
                    }
                    catch (Exception)
                    {
                        // intentionally left blank
                    }
                }

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class ExtractNumbersModelWorkflowClass
    {
        public string PdfPath { get; set; }
        public string? Culture { get; set; }
        public int RandomAmount { get; set; }
        public ScopeDetectionStrategyEnum Strategy { get; set; }
        public Uri ToDb { get; set; }
    }
}
