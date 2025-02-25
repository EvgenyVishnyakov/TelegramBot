using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages.Base
{
    public abstract class CallbackQueryPhotoPageBase(ResourcesService service, ITelegramBotClient client) : CallbackQueryPageBase
    {
        public abstract byte[] GetPhoto();

        public override PageResultBase View(Update update, UserState userState)
        {
            client.SendChatActionAsync(update.CallbackQuery!.Message!.Chat.Id, chatAction: ChatAction.UploadPhoto).Wait();
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
