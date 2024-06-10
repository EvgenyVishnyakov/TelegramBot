using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

class Program
{
      static async Task Main(string[] args)
    {
        var telegramBotClient = new TelegramBotClient("7244274831:AAEKs7B6FHzqLBwWYc6nxv6xad6tUFImgnU");
        var user = await telegramBotClient.GetMeAsync();
        telegramBotClient.StartReceiving(updateHandler: HandleUpdate, pollingErrorHandler: HandlePoolingError);
        
        Console.WriteLine($"Начали слушать апдеты с {user.Username}");
        Console.ReadLine();
    }

    

    private static async Task HandleUpdate(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Message?.Text!=null)
        {
            var chatId = update.Message.Chat.Id; //получаем инфу с update
            var text = update.Message.Text;//update  у него берем 
            await client.SendTextMessageAsync(chatId: chatId, $"Бот ответит вам: \t {text}");
        }
    }

    private static async Task HandlePoolingError(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
    }

}

