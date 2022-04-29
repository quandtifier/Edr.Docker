
using Edr.Api.Interfaces;
using Edr.Api.Domain;
using Edr.Api.Middleware;
using Serilog;
using Serilog.Events;

var assName = typeof(Program).Assembly.GetName().Name;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProperty("Assembly", assName)
            .WriteTo.Seq(serverUrl: "http://host.docker.internal:5341")
            .WriteTo.Console()
            .CreateLogger();

try
{
    Log.Information("Starting web host");
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddScoped<IUserProspectLogic, UserProspectLogic>();
    builder.Services.AddScoped<IStudentLogic, StudentLogic>();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    var app = builder.Build();

    app.UseMiddleware<CustomeExceptionHandlingMiddleware>();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}


