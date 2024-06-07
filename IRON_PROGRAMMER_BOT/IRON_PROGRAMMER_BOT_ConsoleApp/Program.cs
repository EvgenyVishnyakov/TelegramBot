using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

class Program
{
      static void Main(string[] args)
    {
        var telegramBotClient = new TelegramBotClient("7244274831:AAEKs7B6FHzqLBwWYc6nxv6xad6tUFImgnU");
        telegramBotClient.StartReceiving(updateHandler: HandleUpdate, pollingErrorHandler: HandlePoolingError);
    }

    private static async Task HandlePoolingError(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    private static async Task HandleUpdate(ITelegramBotClient client, Update update, CancellationToken token)
    {
        throw new NotImplementedException();
    }


}

