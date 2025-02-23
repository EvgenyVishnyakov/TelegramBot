using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class CommonQuestionsPage(ResourcesService resourcesService) : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            try
            {
                var text = Resources.CommonQuestionsPageText;
                var path = Resources.Фото_ИИ;
                var photo = resourcesService.GetResource(path, "Фото ИИ");
                var replyMarkup = GetKeyboard();
                userState.AddPage(this);

                return new PhotoPageResult(photo, text, replyMarkup)
                {
                    UpdatedUserState = userState
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе View, файл CommonQuestionsPage");
                return View(update, userState);
            }
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            try
            {
                if (update.CallbackQuery == null)
                    return View(update, userState);
                if (update.CallbackQuery.Data == Resources.Back)
                {
                    userState.Pages.Pop();
                    return userState.CurrenntPage.View(update, userState);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе Handle, файл CommonQuestionsPage");
                return View(update, userState);
            }
            return View(update, userState);
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            try
            {
                var back = InlineKeyboardButton.WithCallbackData(Resources.Back);
                return new InlineKeyboardMarkup(new[]
        {
        new[] { back }
        });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе GetKeyboard, файл CommonQuestionsPage");
                return null;
            }
        }
    }
}