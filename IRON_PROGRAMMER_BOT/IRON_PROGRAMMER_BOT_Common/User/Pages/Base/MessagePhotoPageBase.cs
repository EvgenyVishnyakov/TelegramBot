using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages.Base
{
    public abstract class MessagePhotoPageBase(ResourcesService resourcesService) : CallbackQueryPhotoPageBase(resourcesService)
    {
        public abstract UserState ProcessMessage(Message message, UserState userState);

        public abstract IPage CetNextPage();

        public override PageResultBase Handle(Update update, UserState userState)
        {
            try
            {
                if (update.Message == null)
                    return base.Handle(update, userState);
                var updateUserState = ProcessMessage(update.Message, userState);
                var nextPage = CetNextPage();

                return nextPage.View(update, updateUserState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return base.Handle(update, userState);
            }
        }
    }
}
