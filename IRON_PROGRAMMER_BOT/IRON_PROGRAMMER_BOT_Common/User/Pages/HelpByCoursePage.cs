using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.Base;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class HelpByCoursePage(IServiceProvider services, ResourcesService resourcesService, ITelegramService telegramService) : CallbackQueryPhotoPageBase(resourcesService, telegramService)
    {

        public override byte[] GetPhoto()
        {
            return Resources.ThinlingAI;
        }

        public override string GetText(UserState userState)
        {
            return Resources.HelpByCoursePageText;
        }

        public override ButtonLinkPage[][] GetKeyBoardAsync()
        {
            try
            {
                return [
                    [
                     new ButtonLinkPage(InlineKeyboardButton.WithCallbackData("Общий вопрос по изучаемой теме", Resources.CommonQuestionsPage), services.GetRequiredService<CommonQuestionsPage>())
                        ],
                        [
                    new ButtonLinkPage(InlineKeyboardButton.WithCallbackData("Вопрос по конкретной задаче",Resources.ResolveTaskPage), services.GetRequiredService<ResolveTaskPage>())
                    ],
                    [
                    new ButtonLinkPage(InlineKeyboardButton.WithCallbackData(Resources.Back),services.GetRequiredService<BackwardDummyPage>())
                            ]
                    ];
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка {ex} в методе GetKeyboard, файл HelpByCoursePage");
                return null;
            }
        }
    }
}