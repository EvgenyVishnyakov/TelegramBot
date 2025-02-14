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


        if (update.Message?.Text != null)
        {
            var data = update.Message.Text.Split();
            if (data[0] == "/inline_buttons" && data.Length == 3)
            {
                var first_size = int.Parse(data[1]);
                var second_size = int.Parse(data[2]);

                var buttons = GetInlineButtons(first_size, second_size);

                var chatId = update.Message.Chat.Id;
                var text = update.Message.Text;
                var messageId = update.Message.MessageId;

                await client.SendTextMessageAsync(chatId: chatId, text: $"Вы прислали: \n {text}",
                        replyMarkup: new InlineKeyboardMarkup(buttons));

            }
        }
    }

    private static List<List<InlineKeyboardButton>> GetInlineButtons(int first_size, int second_size)
    {
        var buttons = new List<List<InlineKeyboardButton>>();

        var counterButtons = 1;

        for (int i = 0; i < first_size; i++)
        {
            var row = new List<InlineKeyboardButton>();
            for (int j = 0; j < second_size; j++)
            {
                row.Add(new InlineKeyboardButton(counterButtons.ToString())
                {
                    CallbackData = "Успешно"
                }
                );
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

