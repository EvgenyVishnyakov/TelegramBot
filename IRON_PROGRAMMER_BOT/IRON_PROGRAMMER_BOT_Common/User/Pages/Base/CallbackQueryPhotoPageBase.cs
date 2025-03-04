using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Serilog;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages.Base
{
    public abstract class CallbackQueryPhotoPageBase(ResourcesService service, ITelegramService telegramService) : CallbackQueryPageBase(telegramService)
    {
        public abstract byte[] GetPhoto();

        public override PageResultBase View(Update update, UserState userState)
        {
            try
            {
                if (update.CallbackQuery != null)
                    telegramService.SendChatPhotoActionAsync(update).GetAwaiter();

                var text = GetText(userState);
                var keyboard = GetInlineKeyboardMarkup();
                var photo = service.GetResource(GetPhoto());
                userState.AddPage(this);

                var path = Resources.Logo;

                return new PhotoPageResult(photo, text, keyboard)
                {
                    UpdatedUserState = userState
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }
    }
}
