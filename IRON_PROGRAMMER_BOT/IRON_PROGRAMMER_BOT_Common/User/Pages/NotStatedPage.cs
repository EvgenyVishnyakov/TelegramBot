using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class NotStatedPage : IPage
    {
        public PageResultBase Handle(Update update, UserState userState)
        {
            return new StartPage().View(update, userState);
        }

        public PageResultBase View(Update update, UserState userState)
        {
            return new StartPage().View(update, userState);
        }
    }
}
