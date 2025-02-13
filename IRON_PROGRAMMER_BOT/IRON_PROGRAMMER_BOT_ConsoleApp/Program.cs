﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

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

        var data = update.Message.Text.Split();
        var chatId = update.Message.Chat.Id;
        if (data[0] == "/photo" && data.Length == 2)
        {
            if (data.Length == 2)
            {
                var pathPhoto = data[1];


                var text = update.Message.Text;
                var messageId = update.Message.MessageId;

                await client.SendPhotoAsync(
                    chatId: chatId,
                    InputFile.FromUri(pathPhoto),
                    caption: "Вот ваша фотка"
                    );
            }
            else
            {
                var listPhoto = new string[]{
                    "Фото1.jpg",
                    "Фото2.jpg",
                    "Фото3.jpg",
                    "Фото4.png"
                };
                var random = new Random();
                var photoIndex = random.Next(listPhoto.Length);

                using (var file = new FileStream($@"\Images\{listPhoto[photoIndex]}", FileMode.Open, FileAccess.Read))
                {
                    await client.SendPhotoAsync(
                        chatId: chatId,
                        InputFile.FromStream(file),
                        caption: listPhoto[photoIndex]
                        );
                }
            }

        }

    }


    private static async Task HandlePollingError(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
    }
}

