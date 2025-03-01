using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.Base;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class InfoByCoursePage(IServiceProvider services, ResourcesService resourcesService, ITelegramService telegramService) : CallbackQueryPhotoPageBase(resourcesService, telegramService)
    {
        public override byte[] GetPhoto()
        {
            return Resources.Logo;
        }

        public override string GetText(UserState userState)
        {
            return Resources.InfoByCoursePageText;
        }

        public override ButtonLinkPage[][] GetKeyBoardAsync()
        {
            return [
                [
                new ButtonLinkPage(InlineKeyboardButton.WithUrl("Переход в школу", Resources.GoToSchool),null)
                    ],
                    [
                        new ButtonLinkPage(InlineKeyboardButton.WithCallbackData(Resources.Back),services.GetRequiredService<BackwardDummyPage>())
                        ]

                ];
        }
    }
}
