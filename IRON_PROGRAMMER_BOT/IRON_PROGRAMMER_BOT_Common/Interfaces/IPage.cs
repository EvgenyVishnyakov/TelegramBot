using IRON_PROGRAMMER_BOT_Common.User;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.Interfaces
{
    public interface IPage
    {
        PageResultBase View(Update update, UserState userState);

        PageResultBase Handle(Update update, UserState userState);
    }
}
