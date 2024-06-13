using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.WebRequestMethods;

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



    static async Task HandleUpdate(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Message?.Text != null)
        {
            var text = update.Message.Text;
            var chatId = update.Message.Chat.Id;

            var parts = text.Split(' ');
            if (parts.Length == 3 && parts[0] == "/buttons" && int.TryParse(parts[1], out int n) && int.TryParse(parts[2], out int m))
            {
                var buttons = new KeyboardButton[n][];
                int counter = 1;
                for (int i = 0; i < n; i++)
                {
                    buttons[i] = new KeyboardButton[m];
                    for (int j = 0; j < m; j++)
                    {
                        buttons[i][j] = new KeyboardButton(counter.ToString());
                        counter++;
                    }
                }

                var replyMarkup = new ReplyKeyboardMarkup(buttons);

                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Кнопки",
                    replyMarkup: replyMarkup
                );
            }
            else
            {
                await client.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Пожалуйста, используйте /buttons n m, где n и m - это целые числа."
                );
            }
        }

    }

    private static async Task HandlePoolingError(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
    }

}

