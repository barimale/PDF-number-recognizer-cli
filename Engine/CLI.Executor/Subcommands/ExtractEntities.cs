using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TR.Engine;
using TR.Engine.Contract;

namespace CLI.PdfExtractor.Subcommands
{
    [Command("extract-numbers",
        UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect,
        Description = "Use engine to extract numbers")]
    [HelpOption("-?")]
    public class ExtractEntities
    {
        private readonly ILogger<ExtractEntities> _logger;

        public ExtractEntities(ILogger<ExtractEntities> logger)
        {
            _logger = logger;
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
        [Range(1, 500)]
        private int RandomAmount { get; } = 5;

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

                var engine = app.GetRequiredService<IExtractNumbersService>();

                if (string.IsNullOrEmpty(PdfPath))
                    return -1;

                var inputData = new ExtractNumbersModelWorkflowClass()
                {
                    PdfPath = PdfPath,
                    Culture = LanguageCulture,
                    RandomAmount = RandomAmount
                };

                var results = engine.Execute(inputData);

                foreach (var item in results)
                {
                    Console.WriteLine("For the text line: " + item.Key);

                    Console.WriteLine(item.Value.Any() ? $"I found the following entities ({item.Value.Count():d}):" : "I found no entities.");
                    item.Value.ToList().ForEach(result => Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented)));
                    Console.WriteLine();
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
