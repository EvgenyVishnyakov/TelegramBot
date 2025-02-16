using System;
using System.Threading;
using System.Threading.Tasks;
using IRON_PROGRAMMER_BOT_ConsoleApp.Storage;
using IRON_PROGRAMMER_BOT_ConsoleApp.User;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

class Program
{
    static UserStateStorage stateStorage = new UserStateStorage();
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
        if (GetTypeUpdate(update))
        {
            return;
        }

        long telegramUserId = GetUserId(update);
        Console.WriteLine($"updateId={update.Id}, telegramUserId={telegramUserId}");

        var isExistUserState = stateStorage.TryGet(telegramUserId, out var userState);
        if (!isExistUserState)
        {
            userState = new UserState(new NotStatedPage(), new UserData());
        }
        Console.WriteLine($"updated_Id={update.Id}, userState={userState}");

        var result = userState!.Page.Handle(update, userState);
        Console.WriteLine($"updated_Id={update.Id}, send_text={result.Text}, Updated_UserState = {result.UpdatedUserState}");

        var lastMessage = await SendResult(client, telegramUserId, result, update, isExistUserState);

        result.UpdatedUserState.UserData.LastMessage = new IRON_PROGRAMMER_BOT_ConsoleApp.User.Message(lastMessage.MessageId, result.IsMedia);
        stateStorage.AddOrUpdate(telegramUserId, result.UpdatedUserState);
    }

    private static bool GetTypeUpdate(Update update)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                {
                    return update.Message == null;
                }
            case UpdateType.CallbackQuery:
                {
                    return update.CallbackQuery == null;
                }
            default:
                {
                    throw new Exception("Неподдерживаемый тип обновления.");
                }
        }
    }

    private static long GetUserId(Update update)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                {
                    return update.Message.Chat.Id;
                }
            case UpdateType.CallbackQuery:
                {
                    return update.CallbackQuery.Message.Chat.Id;
                }
            default:
                {
                    throw new Exception("Неподдерживаемый тип обновления.");
                }
        }
    }

    private static async Task<Telegram.Bot.Types.Message> SendResult(ITelegramBotClient client, long telegramUserId, PageResultBase result, Update update, bool isExistUserState)
    {
        switch (result)
        {
            case PhotoPageResult photoPageResult:
                return await SendPhoto(client, telegramUserId, photoPageResult, update);
            //case VideoPageResult videoPageResult:
            //    await client.SendVideoAsync(
            //        chatId: telegramUserId,
            //        video: videoPageResult.Video,
            //        caption: videoPageResult.Text,
            //        replyMarkup: videoPageResult.ReplyMarkup,
            //        parseMode: ParseMode.Html
            //        );

            //case AudioPageResult audioPageResult:
            //    await client.SendAudioAsync(
            //        chatId: telegramUserId,
            //        audio: audioPageResult.Audio,
            //        caption: audioPageResult.Text,
            //        replyMarkup: audioPageResult.ReplyMarkup,
            //        parseMode: ParseMode.Html
            //        );

            //case DocumentPageResult documentPageResult:
            //    await client.SendDocumentAsync(
            //        chatId: telegramUserId,
            //        document: documentPageResult.Document,
            //        caption: documentPageResult.Text,
            //        replyMarkup: documentPageResult.ReplyMarkup
            //        );

            default:
                return await SendText(client, telegramUserId, result, update, isExistUserState);

        }
    }

    private static async Task<Telegram.Bot.Types.Message> SendPhoto(ITelegramBotClient client, long telegramUserId, PhotoPageResult photoPageResult, Update update)
    {
        if (update.CallbackQuery != null && (photoPageResult.UpdatedUserState.UserData.LastMessage?.IsMedia ?? false))
        {
            return await client.EditMessageMediaAsync(
                chatId: telegramUserId,
                messageId: photoPageResult.UpdatedUserState.UserData.LastMessage!.Id,
                 media: new InputMediaPhoto(photoPageResult.Photo)
                 {
                     Caption = photoPageResult.Text,
                     ParseMode = photoPageResult.ParseMode
                 },
                 replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup?)photoPageResult.ReplyMarkup

                );
        }

        if (photoPageResult.UpdatedUserState.UserData.LastMessage != null)
        {
            await client.DeleteMessageAsync(
                chatId: telegramUserId,
                messageId: photoPageResult.UpdatedUserState.UserData.LastMessage!.Id);
        }

        return await client.SendPhotoAsync(
            chatId: telegramUserId,
            photo: photoPageResult.Photo,
            caption: photoPageResult.Text,
            replyMarkup: photoPageResult.ReplyMarkup,
            parseMode: ParseMode.Html
            );

    }

    private static async Task<Telegram.Bot.Types.Message> SendText(ITelegramBotClient client, long telegramUserId, PageResultBase result, Update update, bool isExistUserState)
    {
        if (update.CallbackQuery != null && (!result.UpdatedUserState.UserData.LastMessage?.IsMedia ?? false))
        {
            return await client.EditMessageTextAsync(
              chatId: telegramUserId,
              messageId: result.UpdatedUserState.UserData.LastMessage!.Id,
              text: result.Text,
              replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)result.ReplyMarkup,
              parseMode: ParseMode.Html
              );
        }
        if (update.Message == null)
        {
            if (result.UpdatedUserState.UserData.LastMessage != null)
            {
                await client.DeleteMessageAsync(
                    chatId: telegramUserId,
                    messageId: result.UpdatedUserState.UserData.LastMessage!.Id);
            }
        }

        return await client.SendTextMessageAsync(
                    chatId: telegramUserId,
                     text: result.Text,
                    replyMarkup: result.ReplyMarkup,
                    parseMode: ParseMode.Html);
    }

    private static async Task HandlePollingError(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
    }
}


