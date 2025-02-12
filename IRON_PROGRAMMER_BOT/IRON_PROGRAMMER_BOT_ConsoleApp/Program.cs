using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    static async Task Main(string[] args)
    {
        var telegramBotClient = new TelegramBotClient("7239844885:AAHDJcHRUexZj1FtxlTPlI7-XoeFN7yByxg");

        var user = await telegramBotClient.GetMeAsync();
        Console.WriteLine($"Начали слушать updates {user.Username}");

        telegramBotClient.StartReceiving(updateHandler: HandleUpdate, pollingErrorHandler: HandlePollingError);

        Console.ReadLine();
    }

    private static async Task HandleUpdate(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Message?.Text != null)
        {
            var data = update.Message.Text.Split();
            if (data[0] == "/buttons" && data.Length == 3)
            {
                var first_size = int.Parse(data[1]);
                var second_size = int.Parse(data[2]);

                var buttons = GetReplayButtons(first_size, second_size);

                var chatId = update.Message.Chat.Id;
                var text = update.Message.Text;
                var messageId = update.Message.MessageId;

                await client.SendTextMessageAsync(chatId: chatId, text: $"Вы прислали: \n {text}",
                    replyMarkup: new ReplyKeyboardMarkup(buttons)

                    {
                        ResizeKeyboard = true
                    });

            }
        }
    }

    private static List<List<KeyboardButton>> GetReplayButtons(int first_size, int second_size)
    {
        var buttons = new List<List<KeyboardButton>>();

        var counterButtons = 1;

        for (int i = 0; i < first_size; i++)
        {
            var row = new List<KeyboardButton>();
            for (int j = 0; j < second_size; j++)
            {
                row.Add(new KeyboardButton(counterButtons.ToString()));
                counterButtons++;
            }
            buttons.Add(row);
        }

        return buttons;
    }

    private static async Task HandlePollingError(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
    }
}

