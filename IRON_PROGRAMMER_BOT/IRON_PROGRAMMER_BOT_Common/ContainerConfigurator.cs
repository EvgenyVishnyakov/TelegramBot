using Firebase.Database;
using IRON_PROGRAMMER_BOT_Common.Configuration;
using IRON_PROGRAMMER_BOT_Common.Firebase;
using IRON_PROGRAMMER_BOT_Common.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace IRON_PROGRAMMER_BOT_Common
{
    public static class ContainerConfigurator
    {
        public static void Configure(IConfiguration configuration, IServiceCollection service)
        {
            try
            {
                var firebaseConfigurationSection = configuration.GetSection(FirebaseConfiguration.SectionName);
                service.Configure<FirebaseConfiguration>(firebaseConfigurationSection);

                var botConfigurationSection = configuration.GetSection(BotConfiguration.SectionName);
                service.Configure<BotConfiguration>(botConfigurationSection);

                service.AddSingleton<UserStateStorage>();
                service.AddSingleton<FirebaseProvider>();
                service.AddSingleton(services =>
                {
                    var firebaseConfig = services.GetService<IOptions<FirebaseConfiguration>>()!.Value;

                    return new FirebaseClient(firebaseConfig.BasePath, new FirebaseOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(firebaseConfig.Secret)
                    });
                });

                service.AddHttpClient("tgBotClient").AddTypedClient<ITelegramBotClient>((httpClient, services) =>
                {
                    var botConfig = services.GetService<IOptions<BotConfiguration>>()!.Value;
                    var options = new TelegramBotClientOptions(botConfig.BotToken);
                    return new TelegramBotClient(options, httpClient);
                });

                service.AddSingleton<IUpdateHandler, UpdateHandler>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
