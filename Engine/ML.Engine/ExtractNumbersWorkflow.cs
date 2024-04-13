using Microsoft.Recognizers.Text;
using ML.Engine.Contract;
using ML.Engine.Services;
using System.Text;
using TR.Engine.Utilities;

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

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var rnd = new Random();
            var narrowed = output.OrderBy(x => rnd.Next()).Take(_input.RandomAmount).ToList();

            var dic = new Dictionary<string, IEnumerable<ModelResult>>();
            foreach (var item in narrowed)
            {
                var results = PdfParser.ParseAll(item, _input.Culture);
                dic.Add(item, results);
            }

            return dic;
        }
    }

    public class ExtractNumbersModelWorkflowClass
    {
        public string PdfPath { get; set; }
        public string? Culture { get; set; }
        public int RandomAmount { get; set; }
    }
}
