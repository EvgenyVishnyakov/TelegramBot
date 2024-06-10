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
        telegramBotClient.StartReceiving(updateHandler: HandleUpdate, pollingErrorHandler: HandlePoolingError);//ddos каждые 50 секунд

        Console.WriteLine($"Начали слушать апдеты с {user.Username}");
        Console.ReadLine();
    }



    private static async Task HandleUpdate(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Message?.Text != null)
        {
            var chatId = update.Message.Chat.Id;
            var text = update.Message.Text;
            var messageId = update.Message.MessageId;

            //просто сообщение с текстом
            await client.SendTextMessageAsync(
                chatId: chatId,
                text: $"Бот ответит вам: \t {text}");

            //сообщение с цитатой
            await client.SendTextMessageAsync(
                chatId: chatId,
                text: $"Бот ответит вам: \t {text}",
                replyToMessageId: messageId);

        }
    }

    private static async Task HandlePoolingError(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
    }

}

