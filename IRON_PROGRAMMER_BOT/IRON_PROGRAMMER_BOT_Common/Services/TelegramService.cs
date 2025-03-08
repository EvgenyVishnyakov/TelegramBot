using IRON_PROGRAMMER_BOT_Common.Interfaces;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace IRON_PROGRAMMER_BOT_Common.Services
{
    public class TelegramService(ITelegramBotClient botClient) : ITelegramService
    {
        public async Task SendChatPhotoActionAsync(Update update)
        {
            try
            {
                await botClient.SendChatActionAsync(update.CallbackQuery!.Message!.Chat.Id, ChatAction.UploadPhoto);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public async Task SendChatTypingActionAsync(Update update)
        {
            try
            {
                await botClient.SendChatActionAsync(update.Message!.Chat.Id, ChatAction.Typing);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public async Task SendChatTypingCallbackQueryActionAsync(Update update)
        {
            try
            {
                await botClient.SendChatActionAsync(update.CallbackQuery!.Message!.Chat.Id, ChatAction.Typing);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
    }
}
