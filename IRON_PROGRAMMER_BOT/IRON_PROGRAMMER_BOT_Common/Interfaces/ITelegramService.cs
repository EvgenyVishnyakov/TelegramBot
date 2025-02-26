using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.Interfaces
{
    public interface ITelegramService
    {
        Task SendChatActionAsync(Update update);
    }
}
