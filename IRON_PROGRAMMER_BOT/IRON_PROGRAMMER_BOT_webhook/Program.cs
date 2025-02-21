using Firebase.Database;
using IRON_PROGRAMMER_BOT_ConsoleApp.Configuration;
using IRON_PROGRAMMER_BOT_ConsoleApp.Firebase;
using IRON_PROGRAMMER_BOT_ConsoleApp.Storage;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var firebaseConfigurationSection = builder.Configuration.GetSection(FirebaseConfiguration.SectionName);
builder.Services.Configure<FirebaseConfiguration>(firebaseConfigurationSection);

var botConfigurationSection = builder.Configuration.GetSection(BotConfiguration.SectionName);
builder.Services.Configure<BotConfiguration>(botConfigurationSection);

builder.Services.AddSingleton<UserStateStorage>();
builder.Services.AddSingleton<FirebaseProvider>();
builder.Services.AddSingleton<FirebaseClient>(services =>
{
    var firebaseConfig = services.GetService<IOptions<FirebaseConfiguration>>()!.Value;

    return new FirebaseClient(firebaseConfig.BasePath, new FirebaseOptions
    {
        AuthTokenAsyncFactory = () => Task.FromResult(firebaseConfig.Secret)
    });
});

builder.Services.AddHttpClient("tgBotClient").AddTypedClient<ITelegramBotClient>((httpClient, services) =>
{
    var botConfig = services.GetService<IOptions<BotConfiguration>>()!.Value;
    var options = new TelegramBotClientOptions(botConfig.BotToken);
    return new TelegramBotClient(options, httpClient);
});

builder.Services.AddSingleton<IUpdateHandler, UpdateHandler>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
