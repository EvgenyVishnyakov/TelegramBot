using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.Interfaces
{
    public interface ITelegramService
    {
        Task SendChatPhotoActionAsync(Update update);
        Task SendChatTypingActionAsync(Update update);
    }
}
