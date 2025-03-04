using IRON_PROGRAMMER_BOT_Common;
using IRON_PROGRAMMER_BOT_Webhook;
using Serilog;

try
{
    Log.Information("starting server.");
    Log.Logger = new LoggerConfiguration().WriteTo.File("bin/debug/net9.0/Logs/log.json")
    .CreateLogger();

    var builder = WebApplication.CreateBuilder(args);

    ContainerConfigurator.Configure(builder.Configuration, builder.Services);

    builder.Services.AddHostedService<WebHookConfigurator>();

    builder.Services.AddControllers().AddNewtonsoftJson();

    var app = builder.Build();

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "server terminated unexpectedly");
}

finally
{
    Log.CloseAndFlush();
}