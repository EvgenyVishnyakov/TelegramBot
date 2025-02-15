using System;
using System.Threading;
using System.Threading.Tasks;
using IRON_PROGRAMMER_BOT_ConsoleApp.Storage;
using IRON_PROGRAMMER_BOT_ConsoleApp.User;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot;
using Telegram.Bot.Types;

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

        if (update.Message == null)
        {
            return;
        }

        var telegramUserId = update.Message.From.Id;
        Console.WriteLine($"updateId={update.Id}, telegramUserId={telegramUserId}");

        var isExistUserState = stateStorage.TryGet(telegramUserId, out var userState);
        if (!isExistUserState)
        {
            userState = new UserState(new NotStatedPage(), new UserData());
        }
        Console.WriteLine($"updated_Id={update.Id}, userState={userState}");

        var result = userState!.Page.Handle(update, userState);
        Console.WriteLine($"updated_Id={update.Id}, send_text={result.Text}, Updated_UserState = {result.UpdatedUserState}");

        await GetUpdate(client, telegramUserId, result);

        stateStorage.AddOrUpdate(telegramUserId, result.UpdatedUserState);
    }

    private static async Task GetUpdate(ITelegramBotClient client, long telegramUserId, PageResultBase result)
    {
        switch (result)
        {
            case PhotoPageResult photoPageResult:
                await client.SendPhotoAsync(
                    chatId: telegramUserId,
                    photo: photoPageResult.Photo,
                    caption: photoPageResult.Text,
                    replyMarkup: photoPageResult.ReplyMarkup
                    );
                break;
            case VideoPageResult videoPageResult:
                await client.SendVideoAsync(
                    chatId: telegramUserId,
                    video: videoPageResult.Video,
                    caption: videoPageResult.Text,
                    replyMarkup: videoPageResult.ReplyMarkup
                    );
                break;
            case AudioPageResult audioPageResult:
                await client.SendAudioAsync(
                    chatId: telegramUserId,
                    audio: audioPageResult.Audio,
                    caption: audioPageResult.Text,
                    replyMarkup: audioPageResult.ReplyMarkup
                    );
                break;
            case DocumentPageResult documentPageResult:
                await client.SendDocumentAsync(
                    chatId: telegramUserId,
                    document: documentPageResult.Document,
                    caption: documentPageResult.Text,
                    replyMarkup: documentPageResult.ReplyMarkup
                    );
                break;
            default:
                await client.SendTextMessageAsync(
           chatId: telegramUserId,
           text: result.Text,
           replyMarkup: result.ReplyMarkup);
                break;
        }
    }

    private static async Task HandlePollingError(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
    }
}


