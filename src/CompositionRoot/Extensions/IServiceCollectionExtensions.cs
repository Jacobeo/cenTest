using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace CompositionRoot.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterSerilog(this IServiceCollection services)
        {
            // Register ILogger with Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            // Register ILogger as a singleton with SimpleInjector
            services.AddSingleton(Log.Logger);

            return services;
        }
    }
}
