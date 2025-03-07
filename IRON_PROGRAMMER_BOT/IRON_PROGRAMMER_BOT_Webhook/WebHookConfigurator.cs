using IRON_PROGRAMMER_BOT_Common.Configuration;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace IRON_PROGRAMMER_BOT_Webhook
{
    public class WebHookConfigurator(ITelegramBotClient botClient, IOptions<BotConfiguration> botConfiguration, IGigaChatApiProvider gigaChatApiProvider) : IHostedService
    {
        private readonly BotConfiguration _botConfiguration = botConfiguration.Value;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var webhookAddress = _botConfiguration.HostAddress + BotConfiguration.UpdateRoute;
            await gigaChatApiProvider.AuthenticateAsync();

            await botClient.SetWebhookAsync(url: webhookAddress, secretToken: _botConfiguration.SecretToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await botClient.DeleteWebhookAsync();
        }
    }
}
