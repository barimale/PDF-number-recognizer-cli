using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TR.Engine;
using TR.Engine.Contract;

namespace CLI.PdfExtractor.Subcommands
{
    [Command("count-lines",
             UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect,
             Description = "count lines of pdf document")]
    [HelpOption("-?")]
    public class CountLines
    {
        private readonly ILogger<CountLines> _logger;
        private readonly IConsole _console;

        public CountLines(ILogger<CountLines> logger, IConsole console)
        {
            _logger = logger;
            _console = console;
        }

        [FileExists]
        [Argument(0, Description = "input path to PDF document")]
        private string PdfPath { get; } = string.Empty;

        private async Task<int> OnExecuteAsync(
                                CommandLineApplication app,
                                CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(PdfPath))
                {
                    app.ShowHelp();
                    return 0;
                }

                var engine = app.GetRequiredService<ICountLinesWorkflow>();

                var inputData = new CountLinesWorkflowClass()
                {
                    OutputModelPath = PdfPath,
                };

                var linesCount = engine.Execute(inputData);
                _console.WriteLine($"Lines count: {linesCount}");

                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return 1;
            }
        }
    }
}
