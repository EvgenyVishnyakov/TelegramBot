using IRON_PROGRAMMER_BOT_Common.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace IRON_PROGRAMMER_BOT_Common.Services
{
    public class TelegramService(ITelegramBotClient botClient) : ITelegramService
    {
        public async Task SendChatActionAsync(Update update)
        {
            await botClient.SendChatActionAsync(update.CallbackQuery!.Message!.Chat.Id, ChatAction.UploadPhoto);
        }
    }
}
