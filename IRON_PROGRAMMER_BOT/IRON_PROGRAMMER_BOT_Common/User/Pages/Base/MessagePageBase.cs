using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages.Base
{
    public abstract class MessagePageBase : CallbackQueryPageBase
    {
        public abstract UserState ProcessMessageAsync(Message message, UserState userState);

        public abstract IPage GetNextPage();

        public override PageResultBase Handle(Update update, UserState userState)
        {
            try
            {
                if (update.Message == null)//update.Message.Text == null || 
                    return base.Handle(update, userState);
                var updateUserState = ProcessMessageAsync(update.Message, userState);
                //var nextPage = GetNextPage();
                return base.Handle(update, userState);
                //return nextPage.View(update, updateUserState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return base.Handle(update, userState);
            }
        }
    }
}
