using IRON_PROGRAMMER_BOT_ConsoleApp.Services;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class ResolveTaskPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"<b>Решение задачи! 💻</b>
<u><i>Отправьте , пожалуйста:</i></u>
<i> - ссылку на задачу</i>
<i> - ссылку на Ваше решение</i>
<i> - Ваш вопрос</i>";

            var path = "Resources//Photos//Фото ИИ.jpg";
            var replyMarkup = GetKeyboard();
            var resource = ResourcesService.GetResource(path);
            return new PhotoPageResult(resource, text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            if (update.CallbackQuery == null)
                return new PageResultBase("Выберите действие с помощью кнопок", GetKeyboard());
            if (update.CallbackQuery.Data == "Назад")
            {
                return new HelpByCoursePage().View(update, userState);
            }
            return null;
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            var button1 = InlineKeyboardButton.WithCallbackData("Назад", "Назад");
            return new InlineKeyboardMarkup(new[]
    {
        new[] { button1 }
        });
        }
    }
}