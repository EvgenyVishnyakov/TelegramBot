using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class HelpByCoursePage(IServiceProvider services, ResourcesService resourcesService) : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            try
            {
                var text = Resources.HelpByCoursePageText;
                var path = Resources.ИИ;
                var replyMarkup = GetKeyboard();
                var resource = resourcesService.GetResource(path, "ИИ");
                userState.AddPage(this);

                return new VideoPageResult(resource, text, replyMarkup)
                {
                    UpdatedUserState = userState
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе View, файл HelpByCoursePage");
                return View(update, userState);
            }
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            try
            {
                if (update.Message != null)
                {
                    return View(update, userState);
                }
                if (update.CallbackQuery!.Data == Resources.Back)
                {
                    userState.Pages.Pop();
                    return userState.CurrenntPage.View(update, userState);
                }
                if (update.CallbackQuery.Data == Resources.CommonQuestionsPage)
                {
                    return services.GetRequiredService<CommonQuestionsPage>().View(update, userState);
                }
                if (update.CallbackQuery.Data == Resources.ResolveTaskPage)
                {
                    return services.GetRequiredService<ResolveTaskPage>().View(update, userState);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе Handle, файл HelpByCoursePage");
                return View(update, userState);
            }

            return View(update, userState);
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            try
            {
                var commonQuestion = InlineKeyboardButton.WithCallbackData("Общий вопрос по изучаемой теме", Resources.CommonQuestionsPage);
                var taskQuestion = InlineKeyboardButton.WithCallbackData("Вопрос по конкретной задаче", Resources.ResolveTaskPage);
                var back = InlineKeyboardButton.WithCallbackData(Resources.Back);

                return new InlineKeyboardMarkup(new[]
        {
        new[] { commonQuestion },
        new[] { taskQuestion },
        new[] {back }
        });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе GetKeyboard, файл HelpByCoursePage");
                return null;
            }
        }
    }
}