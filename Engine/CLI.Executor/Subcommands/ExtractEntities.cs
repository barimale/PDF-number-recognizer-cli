using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TR.Engine;
using TR.Engine.Contract;
using TR.Engine.Model;
using TR.Engine.Utilities;

namespace CLI.PdfExtractor.Subcommands
{
    [Command("extract-numbers",
        UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect,
        Description = "Use engine to extract numbers")]
    [HelpOption("-?")]
    public class ExtractEntities
    {
        private readonly ILogger<ExtractEntities> _logger;
        private readonly IConsole _console;

        public ExtractEntities(ILogger<ExtractEntities> logger, IConsole console)
        {
            _logger = logger;
            _console = console;
        }

        [FileExists]
        [Argument(0,
            Description = "input path to PDF document")]
        private string PdfPath { get; } = string.Empty;

        [Option("-l",
            Description = "culture language")]
        private string? LanguageCulture { get; } = null;

        [Option("-n",
             Description = "random amount")]
        [Range(1, 500000)]
        private int RandomAmount { get; } = 5;

        [Option("-s",
         Description = "strategy")]
        [McMaster.Extensions.CommandLineUtils.AllowedValues("ExecuteRandom", "ExecuteTop1000OrAll", "ExecuteAll", IgnoreCase = false)]
        public string Strategy { get; } = "ExecuteRandom";

        private async Task<int> OnExecuteAsync(
                    CommandLineApplication app,
                    CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(PdfPath) || string.IsNullOrEmpty(Strategy))
                {
                    app.ShowHelp();
                    return 0;
                }

                var engine = app.GetRequiredService<IExtractNumbersService>();

                if (string.IsNullOrEmpty(PdfPath))
                    return -1;

                var inputData = new ExtractNumbersModelWorkflowClass()
                {
                    PdfPath = PdfPath,
                    Culture = LanguageCulture,
                    RandomAmount = RandomAmount,
                    Strategy = Strategy.ParseEnum<ScopeDetectionStrategyEnum>()
                };

                var results = engine.Execute(inputData);

                foreach (var item in results)
                {
                    _console.WriteLine("For the text line: " + item.Key);

                    _console.WriteLine(item.Value.Any() ? $"I found the following entities ({item.Value.Count():d}):" : "I found no entities.");
                    item.Value.ToList().ForEach(result => _console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented)));
                    _console.WriteLine();
                }

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
