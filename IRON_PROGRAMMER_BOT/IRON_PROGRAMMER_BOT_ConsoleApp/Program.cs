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
        string paramBot = Environment.GetEnvironmentVariable("paramBot")!;
        var telegramBotClient = new TelegramBotClient(paramBot);

        var user = await telegramBotClient.GetMeAsync();
        Console.WriteLine($"Начали слушать updates {user.Username}");

        telegramBotClient.StartReceiving(updateHandler: HandleUpdate, pollingErrorHandler: HandlePollingError);

        Console.ReadLine();
    }

    private static async Task HandleUpdate(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Message?.Text != null)
        {
            if (update.Message.Text.StartsWith("/buttons"))
            {
                var data = update.Message.Text.Split();
                if (data.Length == 3)
                {
                    long chatId;
                    string text;
                    GetDataBYMessage(update, out chatId, out text);
                    if (int.TryParse(data[1], out var countRows) && int.TryParse(data[2], out var countColumns))
                        await GetReplyUserButtons(client, chatId, text, countRows, countColumns);
                    else
                        await client.SendTextMessageAsync(chatId: chatId, text: $"Вы прислали неверные числовые данные");
                }
            }
        }
    }

    private static async Task GetReplyUserButtons(ITelegramBotClient client, long chatId, string text, int countRows, int countColumns)
    {
        var buttons = GetReplayButtons(countRows, countColumns);
        await client.SendTextMessageAsync(chatId: chatId, text: $"Вы прислали: \n {text}",
            replyMarkup: new ReplyKeyboardMarkup(buttons)
            {
                ResizeKeyboard = true
            });
    }

    private static void GetDataBYMessage(Update update, out long chatId, out string text)
    {
        chatId = update.Message.Chat.Id;
        text = update.Message.Text;
        var messageId = update.Message.MessageId;
    }

    private static List<List<KeyboardButton>> GetReplayButtons(int first_size, int second_size)
    {
        var buttons = new List<List<KeyboardButton>>();

        var buttonsCounter = 1;

        for (int i = 0; i < first_size; i++)
        {
            var row = new List<KeyboardButton>();
            for (int j = 0; j < second_size; j++)
            {
                row.Add(new KeyboardButton(buttonsCounter.ToString()));
                buttonsCounter++;
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

