using Edr.App.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    var builder = WebApplication.CreateBuilder(args);

    //dump the config to logs: check the seq UI
    var debugView = (builder.Configuration as IConfigurationRoot).GetDebugView();
    Log.ForContext("ConfigurationDebug", debugView)
        .Information("Configuration dump.");

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("Db");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");

}
finally
{
    Log.CloseAndFlush();
}

