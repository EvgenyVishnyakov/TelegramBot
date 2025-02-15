using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class InfoByCoursePage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Информация о курсах!
Вы можете перейти на страницу школы IRON PROGRAMMER";
            var replyMarkup = GetKeyboard();

            return new PageResultBase(text, replyMarkup)
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
                return new StartPage().View(update, userState);
            }
            return null;
        }

        private InlineKeyboardMarkup GetKeyboard()
        {

            var button1 = InlineKeyboardButton.WithUrl("Переход в школу", "https://ironprogrammer.ru/#rec460811109");
            var button2 = InlineKeyboardButton.WithCallbackData("Назад", "Назад");
            return new InlineKeyboardMarkup(new[]
    {
        new[] { button1 },
        new[] { button2 }
        });
        }
    }
}
