using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages.Base
{
    public class BackwardDummyPage : CallbackQueryPageBase
    {
        public override PageResultBase View(Update update, UserState userState)
        {
            userState.Pages.Pop();
            return userState.CurrentPage.View(update, userState);
        }

        public override ButtonLinkPage[][] GetKeyBoardAsync()
        {
            throw new NotImplementedException();
        }

        public override string GetText(UserState userState)
        {
            throw new NotImplementedException();
        }
    }
}
