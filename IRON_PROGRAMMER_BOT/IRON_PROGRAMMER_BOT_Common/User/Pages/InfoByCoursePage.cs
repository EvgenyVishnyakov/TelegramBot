using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class InfoByCoursePage(ResourcesService resourcesService) : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            try
            {
                var text = Resources.InfoByCoursePageText;
                var replyMarkup = GetKeyboard();
                userState.AddPage(this);

                var path = Resources.Логотип;
                var photo = resourcesService.GetResource(path, "Логотип");

                return new PhotoPageResult(photo, text, replyMarkup)
                {
                    UpdatedUserState = userState
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе View, файл InfoByCoursePage");
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе Handle, файл InfoByCoursePage");
                return View(update, userState);
            }
            return View(update, userState);
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            try
            {
                var refToSchool = InlineKeyboardButton.WithUrl("Переход в школу", Resources.GoToSchool);
                var back = InlineKeyboardButton.WithCallbackData(Resources.Back);
                return new InlineKeyboardMarkup(new[]
        {
        new[] { refToSchool },
        new[] { back }
        });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе GetKeyboard, файл InfoByCoursePage");
                return null;
            }
        }
    }
}
