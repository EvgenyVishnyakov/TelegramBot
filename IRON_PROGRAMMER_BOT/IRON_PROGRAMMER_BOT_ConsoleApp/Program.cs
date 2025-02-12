using System;
using System.Threading.Tasks;
using Telegram.Bot;

class Program
{
    static async Task Main(string[] args)
    {
        var telegramBotClient = new TelegramBotClient("7244274831:AAEKs7B6FHzqLBwWYc6nxv6xad6tUFImgnU");
        var user = await telegramBotClient.GetMeAsync();
        telegramBotClient.StartReceiving(updateHandler: HandleUpdate, pollingErrorHandler: HandlePoolingError);//ddos каждые 50 секунд

        Console.WriteLine($"Начали слушать апдеты с {user.Username}");
        Console.ReadLine();
    }

}

