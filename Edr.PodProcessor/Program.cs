using Edr.PodProcessor;
using Serilog;
using Serilog.Events;

var assName = typeof(Program).Assembly.GetName().Name;
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProperty("Assembly", assName)
            .WriteTo.Seq(serverUrl: "http://seq_in_dc:5341")
            .WriteTo.Console()
            .CreateLogger();
try
{
    Log.Information("Starting host");

    IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

