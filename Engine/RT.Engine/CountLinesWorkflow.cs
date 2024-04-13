using TR.Engine.Contract;
using TR.Engine.Services;

namespace TR.Engine
{
    public class CountLinesWorkflow : ICountLinesWorkflow
    {
        private CountLinesWorkflowClass _input;

        public int? Execute(CountLinesWorkflowClass input)
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
                throw;
            }
        }
    }

    public class CountLinesWorkflowClass
    {
        public string OutputModelPath { get; set; }
    }
}
