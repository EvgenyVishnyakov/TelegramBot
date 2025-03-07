using IRON_PROGRAMMER_BOT_Common.Configuration;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using Microsoft.Extensions.Options;
using Serilog;
using Telegram.Bot;

namespace IRON_PROGRAMMER_BOT_Webhook
{
    public class WebHookConfigurator(ITelegramBotClient botClient, IOptions<BotConfiguration> botConfiguration, IGigaChatApiProvider gigaChatApiProvider) : IHostedService
    {
        private readonly BotConfiguration _botConfiguration = botConfiguration.Value;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var webhookAddress = _botConfiguration.HostAddress + BotConfiguration.UpdateRoute;
                await gigaChatApiProvider.AuthenticateAsync();
                await botClient.SetWebhookAsync(url: webhookAddress, secretToken: _botConfiguration.SecretToken);
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка {ex.ToString()} при запуске webhook");
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await botClient.DeleteWebhookAsync();
        }
    }
}
