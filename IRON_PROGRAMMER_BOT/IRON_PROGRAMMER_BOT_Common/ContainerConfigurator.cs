using System.Reflection;
using Firebase.Database;
using IRON_PROGRAMMER_BOT_Common.Configuration;
using IRON_PROGRAMMER_BOT_Common.Firebase;
using IRON_PROGRAMMER_BOT_Common.GigaChatApi;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace IRON_PROGRAMMER_BOT_Common
{
    public static class ContainerConfigurator
    {
        public static void Configure(IConfiguration configuration, IServiceCollection services)
        {
            try
            {
                Log.Information("starting server.");
                Log.Logger = new LoggerConfiguration()
                .WriteTo.File("bin/debug/net9.0/Logs/log.json")
                .CreateLogger();

                var firebaseConfigurationSection = configuration.GetSection(FirebaseConfiguration.SectionName);
                services.Configure<FirebaseConfiguration>(firebaseConfigurationSection);

                var botConfigurationSection = configuration.GetSection(BotConfiguration.SectionName);
                services.Configure<BotConfiguration>(botConfigurationSection);

                var gigaChatApiConfigurationSection = configuration.GetSection(GigaChatApiConfiguration.SectionName);
                services.Configure<GigaChatApiConfiguration>(gigaChatApiConfigurationSection);

                services.AddSingleton<UserStateStorage>();
                services.AddSingleton<FirebaseProvider>();
                services.AddSingleton<ResourcesService>();
                services.AddSingleton(services =>
                {
                    var firebaseConfig = services.GetService<IOptions<FirebaseConfiguration>>()!.Value;

                    return new FirebaseClient(firebaseConfig.BasePath, new FirebaseOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(firebaseConfig.Secret)
                    });
                });

                services.AddHttpClient("tgBotClient").AddTypedClient<ITelegramBotClient>((httpClient, services) =>
                {
                    var botConfig = services.GetService<IOptions<BotConfiguration>>()!.Value;
                    var options = new TelegramBotClientOptions(botConfig.BotToken);
                    return new TelegramBotClient(options, httpClient);
                });

                services.AddSingleton<IUpdateHandler, UpdateHandler>();

                var assembly = Assembly.GetExecutingAssembly();
                var types = assembly.GetTypes().Where(t => typeof(IPage).IsAssignableFrom(t) && !t.IsAbstract);
                foreach (var type in types)
                {
                    services.AddSingleton(type);
                }
                services.AddSingleton<ResourcesService>();
                services.AddSingleton<AuthorizationRequest>();
                services.AddSingleton<EndPoints>();
                services.AddSingleton<IGigaChatApiProvider, GigaChatApiProvider>();
                services.AddSingleton<ITelegramService, TelegramService>();
                services.AddSingleton<PagesFactory>();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "server terminated unexpectedly");
            }

            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
