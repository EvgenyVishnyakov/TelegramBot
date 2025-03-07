using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages.Base
{
    public abstract class MessagePhotoPageBase(ResourcesService resourcesService, ITelegramService telegramService) : CallbackQueryPhotoPageBase(resourcesService, telegramService)
    {
        public abstract UserState ProcessMessageAsync(Message message, UserState userState);

        public abstract IPage GetNextPage();

        public override PageResultBase Handle(Update update, UserState userState)
        {
            try
            {
                if (update.Message == null)
                    return base.Handle(update, userState);
                var updateUserState = ProcessMessageAsync(update.Message, userState);
                if (updateUserState.requestCounter == 0)
                {
                    var nextPage = GetNextPage();
                    return nextPage.View(update, updateUserState);
                }
                return base.Handle(update, userState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return base.Handle(update, userState);
            }
        }
    }
}
