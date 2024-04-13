using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ML.Engine.Contract;
using TR.Engine;

namespace CLI.Executor
{
    [Command(
        Name = "pdf-extractor",
        Description = "tool for text recgonition inside the PDF documents and extraction entities"),
        Subcommand(typeof(DetectLanguage), typeof(ExtractEntities))
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
                        .AddScoped<IExtractNumbersService, ExtractNumbersWorkflow>()
                        .AddScoped<IDetectLanguageWorkflow, DetectLanguageWorkflow>()
                        .AddSingleton(PhysicalConsole.Singleton);
                })
                .RunCommandLineApplicationAsync<Program>(args);
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
                    var engine = app.GetRequiredService<IDetectLanguageWorkflow>();

                    var inputData = new DetectLanguageWorkflowClass()
                    {
                        OutputModelPath = PdfPath,
                        RandomAmount = RandomAmount
                    };

                    var resultId = engine.Execute(inputData);
                    Console.WriteLine("Detected language:");
                    Console.WriteLine($"{resultId}");

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

            [Argument(0,
                Description = "input path to PDF document")]
            private string PdfPath { get; } = string.Empty;

            [Option("-l",
                Description = "culture language")]
            private string? LanguageCulture { get; } = null;

            [Option("-n",
                 Description = "random amount")]
            private int RandomAmount { get; } = 5;

            private async Task<int> OnExecuteAsync(
                        CommandLineApplication app,
                        CancellationToken cancellationToken = default)
            {
                try
                {
                    var engine = app.GetRequiredService<IExtractNumbersService>();

                    if (string.IsNullOrEmpty(PdfPath))
                        return -1;

                    var inputData = new ExtractNumbersModelWorkflowClass()
                    {
                        PdfPath = PdfPath,
                        Culture = LanguageCulture,
                        RandomAmount = RandomAmount
                    };

                    var resultId = engine.Execute(inputData);

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
