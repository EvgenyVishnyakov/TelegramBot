using IRON_PROGRAMMER_BOT_ConsoleApp;
using IRON_PROGRAMMER_BOT_webhook;

var builder = WebApplication.CreateBuilder(args);

ContainerConfigurator.Configure(builder.Configuration, builder.Services);

builder.Services.AddHostedService<WebHookConfigurator>();

builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
