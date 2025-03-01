using System;
using System.Threading;
using System.Threading.Tasks;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace IRON_PROGRAMMER_BOT_ConsoleApp
{
    public class LongPoolingConfigurator(ITelegramBotClient botClient, IUpdateHandler updateHandler, IGigaChatApiProvider gigaChatApiProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var bot = await botClient.GetMeAsync();

            Console.WriteLine($"Начали слушать апдейты с {bot.Username}");
            await gigaChatApiProvider.AuthenticateAsync();

            await botClient.ReceiveAsync(updateHandler: updateHandler);
        }
    }
}
