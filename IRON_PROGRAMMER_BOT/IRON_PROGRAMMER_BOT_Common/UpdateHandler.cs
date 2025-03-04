using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.Storage;
using IRON_PROGRAMMER_BOT_Common.User;
using IRON_PROGRAMMER_BOT_Common.User.Pages;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace IRON_PROGRAMMER_BOT_Common
{
    public class UpdateHandler(UserStateStorage stateStorage, IServiceProvider services) : IUpdateHandler
    {
        public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            try
            {
                if (!GetUpdateType(update))
                {
                    return;
                }

                long telegramUserId = GetUserId(update);
                Log.Information($"updateId={update.Id}, telegramUserId={telegramUserId}");

                var userState = await stateStorage.TryGetAsync(telegramUserId);
                if (userState == null)
                {
                    userState = new UserState(new Stack<IPage>([services.GetRequiredService<NotStatedPage>()]), new UserData());
                }
                Log.Information($"updated_Id={update.Id}, userState={userState}");

                var result = userState!.CurrentPage.Handle(update, userState);
                Log.Information($"updated_Id={update.Id}, send_text={result.Text}, Updated_UserState = {result.UpdatedUserState}");

                var lastMessage = await SendResult(client, telegramUserId, result, update);

                result.UpdatedUserState.UserData.LastMessage = new HelperBotMessage(lastMessage.MessageId, result.IsMedia);
                await stateStorage.AddOrUpdateAsync(telegramUserId, result.UpdatedUserState);
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка {ex} в методе HandlerUpdate, файл Programm");
            }
        }

        public async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Log.Error(exception.Message);
        }

        private static bool GetUpdateType(Update update)
        {
            if (update.Type == UpdateType.Message || update.Type == UpdateType.CallbackQuery)
                return true;
            return false;
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

        private static async Task<Message?> SendResult(ITelegramBotClient client, long telegramUserId, PageResultBase result, Update update)
        {
            try
            {
                switch (result)
                {
                    case PageResultBas photoPageResult:
                        return await SendPhoto(client, telegramUserId, photoPageResult, update);
                    default:
                        return await SendText(client, telegramUserId, result, update);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка {ex} в методе SendResult, файл Programm");
                return null;
            }
        }

        private static async Task<Message?> SendPhoto(ITelegramBotClient client, long telegramUserId, PageResultBas photoPageResult, Update update)
        {
            try
            {
                if (update.CallbackQuery != null && (photoPageResult.UpdatedUserState.UserData.LastMessage?.IsMedia ?? false))
                {
                    return await client.EditMessageMediaAsync(
                        chatId: telegramUserId,
                        messageId: photoPageResult.UpdatedUserState.UserData.LastMessage!.Id,
                         media: new InputMediaPhoto(photoPageResult.Photo)
                         {
                             Caption = photoPageResult.Text,
                             ParseMode = ParseMode.Html
                         },
                         replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup?)photoPageResult.ReplyMarkup
                        );
                }

                if (photoPageResult.UpdatedUserState.UserData.LastMessage != null)
                {
                    try
                    {
                        await client.DeleteMessageAsync(
                        chatId: telegramUserId,
                        messageId: photoPageResult.UpdatedUserState.UserData.LastMessage!.Id);
                    }
                    catch
                    {
                        return await client.SendPhotoAsync(
                    chatId: telegramUserId,
                    photo: photoPageResult.Photo,
                    caption: photoPageResult.Text,
                    replyMarkup: photoPageResult.ReplyMarkup,
                    parseMode: ParseMode.Html
                    );
                    }
                }

                return await client.SendPhotoAsync(
                    chatId: telegramUserId,
                    photo: photoPageResult.Photo,
                    caption: photoPageResult.Text,
                    replyMarkup: photoPageResult.ReplyMarkup,
                    parseMode: ParseMode.Html
                    );
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка {ex} в методе SendPhoto, файл Programm");
                return null;
            }
        }

        private static async Task<Message?> SendText(ITelegramBotClient client, long telegramUserId, PageResultBase result, Update update)
        {
            try
            {
                if (result.UpdatedUserState.UserData.LastMessage != null)
                {
                    try
                    {
                        await client.DeleteMessageAsync(
                        chatId: telegramUserId,
                        messageId: result.UpdatedUserState.UserData.LastMessage!.Id);
                    }
                    catch
                    {
                        return await client.SendTextMessageAsync(
                            chatId: telegramUserId,
                             text: result.Text,
                            replyMarkup: result.ReplyMarkup,
                            parseMode: ParseMode.Markdown);
                    };
                }


                return await client.SendTextMessageAsync(
                            chatId: telegramUserId,
                             text: result.Text,
                            replyMarkup: result.ReplyMarkup,
                            parseMode: ParseMode.Markdown);
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка {ex} в методе SendText, файл Programm");
                return null;
            }
        }
    }
}