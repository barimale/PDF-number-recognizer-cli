using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TR.Engine.DI;

namespace CLI.PdfExtractor
{
    [Command(
        Name = "pdf-extractor",
        UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect,
        Description = "tool for text recgonition of the PDF documents and extraction entities"),
        Subcommand(
            typeof(Subcommands.DetectLanguage),
            typeof(Subcommands.CountLines),
            typeof(Subcommands.ExtractEntities))
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
                        .AddTREngineServices()
                        .AddSingleton(PhysicalConsole.Singleton);
                })
                .RunCommandLineApplicationAsync<Program>(args);
        }

        private async Task<int> OnExecuteAsync(
            CommandLineApplication app,
            CancellationToken cancellationToken)
        {
            app.ShowHelp();
            return 0;
        }
    }
}
