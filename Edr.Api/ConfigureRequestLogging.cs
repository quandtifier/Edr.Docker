using Serilog;
using Serilog.Events;

namespace Edr.Api
{
    public static class ConfigureRequestLogging
    {
        public static IApplicationBuilder UserCustomRequestLogging(this IApplicationBuilder app)
        {
            return app.UseSerilogRequestLogging(options =>
            {
                // health check exclusion inspiration:
                // https://adrewlock.net/using-serilog-aspnetcore-in-asp-net-core-3......
                options.GetLevel = ExcludeHealthChecks;
                options.EnrichDiagnosticContext = (diagnosticsContext, httpContext) =>
                {
                    diagnosticsContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticsContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"]);
                };
            });
        }

        private static LogEventLevel ExcludeHealthChecks(HttpContext ctx, double _, Exception ex) =>
            ex != null
                ? LogEventLevel.Error
                : ctx.Response.StatusCode > 499
                    ? LogEventLevel.Error
                    : IsHealthCheckEndpoint(ctx)
                        ? LogEventLevel.Verbose
                        : LogEventLevel.Information;

        private static bool IsHealthCheckEndpoint(HttpContext ctx)
        {
            var userAgent = ctx.Request.Headers["User-Agent"].FirstOrDefault() ?? "";
            return ctx.Request.Path.Value.EndsWith("health", StringComparison.CurrentCultureIgnoreCase) ||
                userAgent.Contains("HealthCheck", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
