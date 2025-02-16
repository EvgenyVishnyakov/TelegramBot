using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class InfoByCoursePage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"<b>Информация о курсах!</b>
Вы можете перейти на страницу школы <b><u>IRON PROGRAMMER</u></b>";
            var replyMarkup = GetKeyboard();
            userState.AddPage(this);

            return new PageResultBase(text, replyMarkup)
            {
                UpdatedUserState = userState
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            if (update.CallbackQuery == null)
                return new PageResultBase("Выберите действие с помощью кнопок", GetKeyboard());
            if (update.CallbackQuery.Data == "Назад")
            {
                userState.Pages.Pop();
                return userState.CurrenntPage.View(update, userState);
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
