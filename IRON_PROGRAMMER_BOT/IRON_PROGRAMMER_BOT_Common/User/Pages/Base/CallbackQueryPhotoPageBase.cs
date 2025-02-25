using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages.Base
{
    public abstract class CallbackQueryPhotoPageBase(ResourcesService service, ITelegramBotClient client) : CallbackQueryPageBase
    {
        public abstract byte[] GetPhoto();

        public override PageResultBase View(Update update, UserState userState)
        {
            //client.SendChatActionAsync(chatId: userState.UserData.TelegramId, chatAction: ChatAction.Typing).Wait();
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
