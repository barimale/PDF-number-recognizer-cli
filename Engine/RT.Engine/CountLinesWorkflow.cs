using TR.Engine.Services;
using TR.Engine.Contract;

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
                return null;
            }
        }
    }

    public class CountLinesWorkflowClass
    {
        public string OutputModelPath { get; set; }
    }
}
