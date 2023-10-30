using AspNetCore.Serilog.RequestLoggingMiddleware;
using Microsoft.AspNetCore.Builder;

namespace Backend.CompositionRoot.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSerilog(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();

            return app;
        }
    }
}
