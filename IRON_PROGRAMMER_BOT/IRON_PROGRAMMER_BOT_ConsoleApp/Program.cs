using System;
using System.Collections.Generic;
using System.IO;
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
            long chatId;
            string text;
            GetDataBYMessage(update, out chatId, out text);
            if (update.Message.Text.StartsWith("/buttons"))
            {
                var data = update.Message.Text.Split();
                if (data.Length == 3)
                {

                    if (int.TryParse(data[1], out var countRows) && int.TryParse(data[2], out var countColumns))
                        await GetReplyUserButtons(client, chatId, text, countRows, countColumns);
                    else
                        await client.SendTextMessageAsync(chatId: chatId, text: $"Вы прислали неверные числовые данные");
                }
            }
            if (update.Message.Text.StartsWith("/inline_buttons"))
            {
                var data = update.Message.Text.Split();
                if (data.Length == 3)
                {
                    if (int.TryParse(data[1], out var countRows) && int.TryParse(data[2], out var countColumns))
                    {
                        var buttons = GetInlineButtons(countRows, countColumns);
                        await client.SendTextMessageAsync(chatId: chatId, text: $"Вы прислали: \n {text}",
                            replyMarkup: new InlineKeyboardMarkup(buttons));
                    }
                    else
                        await client.SendTextMessageAsync(chatId: chatId, text: $"Вы прислали неверные числовые данные");
                }
            }
            if (update.Message.Text.StartsWith("/photo"))
            {
                var data = update.Message.Text.Split();
                if (data.Length == 2)
                {
                    var pathPhoto = data[1];
                    await client.SendPhotoAsync(
                        chatId: chatId,
                        InputFile.FromUri(pathPhoto),
                        caption: "Вот ваша фотка"
                        );
                }
                else
                {
                    var photo = GetRandomPhoto();
                    using (var file = new FileStream($@"Images/{photo}", FileMode.Open, FileAccess.Read))
                    {
                        await client.SendPhotoAsync(
                            chatId: chatId,
                            InputFile.FromStream(file),
                            caption: photo
                            );
                    }
                }
            }
        }
    }

    private static string GetRandomPhoto()
    {
        var listPhoto = GetListPhoto();
        var random = new Random();
        var photoIndex = random.Next(listPhoto.Length);

        return listPhoto[photoIndex];
    }


    private static string[] GetListPhoto()
    {
        return new string[]{
                    "Фото1.jpg",
                    "Фото2.jpg",
                    "Фото3.jpg",
                    "Фото4.png"
                };
    }

    private static List<List<InlineKeyboardButton>> GetInlineButtons(int countRows, int countColumns)
    {
        var buttons = new List<List<InlineKeyboardButton>>();

        var counterButtons = 1;

        for (int i = 0; i < countRows; i++)
        {
            var row = new List<InlineKeyboardButton>();
            for (int j = 0; j < countColumns; j++)
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

    private static List<List<KeyboardButton>> GetReplayButtons(int countRows, int countColumns)
    {
        var buttons = new List<List<KeyboardButton>>();

        var buttonsCounter = 1;

        for (int i = 0; i < countRows; i++)
        {
            var row = new List<KeyboardButton>();
            for (int j = 0; j < countColumns; j++)
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


