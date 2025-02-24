using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages.Base
{
    public abstract class CallbackQueryPhotoPageBase(ResourcesService service) : CallbackQueryPageBase
    {
        public abstract byte[] GetPhoto();

        public override PageResultBase View(Update update, UserState userState)
        {
            var text = GetText(userState);
            var keyboard = GetInlineKeyboardMarkup();
            var photo = service.GetResource(GetPhoto());
            userState.AddPage(this);

            var path = Resources.Логотип;

            return new PhotoPageResult(photo, text, keyboard)
            {
                UpdatedUserState = userState
            };
        }

    }
}
