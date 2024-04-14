using Microsoft.Extensions.DependencyInjection;
using TR.Engine.Contract;
using TR.Engine.Services;

namespace TR.Engine.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTREngineServices(this IServiceCollection services)
        {
            return services
                    .AddScoped<ILanguageDetector, LanguageDetector>()
                    .AddScoped<IPdfReaderService, PdfReaderService>()
                    .AddScoped<IPdfParserService, PdfParserService>()
                    .AddScoped<ICountLinesWorkflow, CountLinesWorkflow>()
                    .AddScoped<IExtractNumbersService, ExtractNumbersWorkflow>()
                    .AddScoped<IDetectLanguageWorkflow, DetectLanguageWorkflow>();
        }
    }
}
