using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages.Base
{
    public class BackwardDummyPage(ITelegramService telegramService) : CallbackQueryPageBase(telegramService)
    {
        public override PageResultBase View(Update update, UserState userState)
        {
            userState.Pages.Pop();
            return userState.CurrentPage.View(update, userState);
        }

        public override ButtonLinkPage[][] GetKeyBoardAsync()
        {
            return [];
        }

        public override string GetText(UserState userState)
        {
            return string.Empty;
        }
    }
}
