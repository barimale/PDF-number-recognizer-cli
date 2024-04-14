using TR.Engine.Contract;

namespace TR.Engine
{
    public class CountLinesWorkflow : ICountLinesWorkflow
    {
        private readonly IPdfReaderService _pdfReaderService;

        private CountLinesWorkflowClass _input;

        public CountLinesWorkflow(IPdfReaderService pdfReaderService)
        {
            _pdfReaderService = pdfReaderService;
        }

        public int? Execute(CountLinesWorkflowClass input)
        {
            try
            {
                _input = input;

                var output = _pdfReaderService.ExtractTextFromPDF(_input.OutputModelPath);

                return output.Count;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class CountLinesWorkflowClass
    {
        public string OutputModelPath { get; set; }
    }
}
