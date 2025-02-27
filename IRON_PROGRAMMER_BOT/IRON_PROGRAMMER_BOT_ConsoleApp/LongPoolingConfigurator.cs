using System;
using System.Threading;
using System.Threading.Tasks;
using IRON_PROGRAMMER_BOT_Common.GigaChatApi;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace IRON_PROGRAMMER_BOT_ConsoleApp
{
    public class LongPoolingConfigurator(ITelegramBotClient botClient, IUpdateHandler updateHandler, GigaChatApiProvider gigaChatApiProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var user = await botClient.GetMeAsync();

            Console.WriteLine($"Начали слушать апдейты с {user.Username}");
            await gigaChatApiProvider.AuthenticateAsync();

            await botClient.ReceiveAsync(updateHandler: updateHandler);
        }
    }
}
