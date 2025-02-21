﻿using System;
using System.Threading.Tasks;
using Telegram.Bot;

class Program
{
    //static UserStateStorage stateStorage = new UserStateStorage();
    static async Task Main(string[] args)
    {
        string paramBot = Environment.GetEnvironmentVariable("paramBot")!;
        var telegramBotClient = new TelegramBotClient(paramBot);
        var user = await telegramBotClient.GetMeAsync();
        Console.WriteLine($"Начали слушать updates {user.Username}");

        //telegramBotClient.StartReceiving(updateHandler: HandleUpdate, pollingErrorHandler: HandlePollingError);

        Console.ReadLine();
    }
}

