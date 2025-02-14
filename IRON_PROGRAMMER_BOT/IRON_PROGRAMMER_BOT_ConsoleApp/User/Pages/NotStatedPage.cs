using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class NotStatedPage : IPage
    {
        public PageResult Handle(Update update, UserState userState)
        {
            return new StartPage().View(update, userState);
        }

        public PageResult View(Update update, UserState userState)
        {
            return null;
        }
    }
}
