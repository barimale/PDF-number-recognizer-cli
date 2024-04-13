using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TR.Engine;
using TR.Engine.Contract;

namespace CLI.PdfExtractor
{
    [Command(
        Name = "pdf-extractor",
        UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect,
        Description = "tool for text recgonition of the PDF documents and extraction entities"),
        Subcommand(typeof(DetectLanguage), typeof(LinesCounter), typeof(ExtractEntities))
    ]
    [HelpOption("-?")]
    internal class Program
    {
        public static async Task<int> Main(string[] args)
        {
            return await new HostBuilder()
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConsole();
                })
                .ConfigureServices((context, services) =>
                {
                    services
                        .AddScoped<ICountLinesWorkflow, CountLinesWorkflow>()
                        .AddScoped<IExtractNumbersService, ExtractNumbersWorkflow>()
                        .AddScoped<IDetectLanguageWorkflow, DetectLanguageWorkflow>()
                        .AddSingleton(PhysicalConsole.Singleton);
                })
                .RunCommandLineApplicationAsync<Program>(args);
        }

        private async Task<int> OnExecuteAsync(
            CommandLineApplication app,
            CancellationToken cancellationToken = default)
        {
            app.ShowHelp();
            return 0;
        }

        [Command("language-detect",
             Description = "detect entities language")]
        [HelpOption("-?")]
        private class DetectLanguage
        {
            private readonly ILogger<DetectLanguage> _logger;

            public DetectLanguage(ILogger<DetectLanguage> logger)
            {
                _logger = logger;
            }
            [FileExists]
            [Argument(0,
                            Description = "input path to PDF document")]
            private string PdfPath { get; } = string.Empty;

            [Option("-n",
               Description = "random amount")]
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

                    var engine = app.GetRequiredService<IDetectLanguageWorkflow>();

                    var inputData = new DetectLanguageWorkflowClass()
                    {
                        OutputModelPath = PdfPath,
                        RandomAmount = RandomAmount
                    };

                    var resultCulture = engine.Execute(inputData);
                    Console.WriteLine("Detected language:");
                    Console.WriteLine($"{resultCulture}");

                    return 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                    return 1;
                }
            }
        }

        [Command("count-lines",
             Description = "count lines of pdf document")]
        [HelpOption("-?")]
        private class LinesCounter
        {
            private readonly ILogger<LinesCounter> _logger;

            public LinesCounter(ILogger<LinesCounter> logger)
            {
                _logger = logger;
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
                    Console.WriteLine("Lines count:");
                    Console.WriteLine($"{linesCount}");

                    return 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                    return 1;
                }
            }
        }

        [Command("extract-numbers",
             Description = "Use engine to extract numbers")]
        [HelpOption("-?")]
        private class ExtractEntities
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
}
