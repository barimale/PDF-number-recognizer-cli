using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TR.Engine;
using TR.Engine.Contract;

namespace CLI.PdfExtractor.Subcommands
{
    [Command("language-detect",
            UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect,
            Description = "detect entities language")]
    [HelpOption("-?")]
    public class DetectLanguage
    {
        private readonly ILogger<DetectLanguage> _logger;
        private readonly IConsole _console;

        public DetectLanguage(ILogger<DetectLanguage> logger, IConsole console)
        {
            _logger = logger;
            _console = console;
        }

        [FileExists]
        [Argument(0,
            Description = "input path to PDF document")]
        private string PdfPath { get; } = string.Empty;

        [Option("-n",
           Description = "random amount")]
        private int RandomAmount { get; } = 5;

        [Option("-s",
           Description = "strategy")]
        [AllowedValues("ExecuteRandom", "ExecuteTop1000OrAll", "ExecuteAll", IgnoreCase = false)]
        public string Strategy { get; } = "ExecuteRandom";

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

                var engine = app.GetRequiredService<IDetectLanguageWorkflow>();

                var inputData = new DetectLanguageWorkflowClass()
                {
                    PdfPath = PdfPath,
                    RandomAmount = RandomAmount,
                    Strategy = Strategy
                };

                var resultCulture = engine.Execute(inputData);
                _console.WriteLine($"Detected language: {resultCulture ?? "undefined"}");

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
