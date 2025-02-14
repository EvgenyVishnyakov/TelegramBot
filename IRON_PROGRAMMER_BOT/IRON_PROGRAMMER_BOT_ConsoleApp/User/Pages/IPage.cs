using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public interface IPage
    {
        PageResult View(Update update, UserState userState);

        PageResult Handle(Update update, UserState userState);
    }
}
