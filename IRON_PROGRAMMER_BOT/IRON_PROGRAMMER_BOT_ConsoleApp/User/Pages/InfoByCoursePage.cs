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
            var markup = GetMarkup();
            var replyMarkup = GetReplyKeyboard();

            return new PageResultBase(text, markup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            if (update.Message == null)
                return new PageResultBase("Выберите действие с помощью кнопок", GetReplyKeyboard());
            if (update.Message.Text == "/back {StartPage}")
            {
                return new StartPage().View(update, userState);
            }
            return null;
        }

        private ReplyKeyboardMarkup GetReplyKeyboard()
        {
            return new ReplyKeyboardMarkup(
                [
                    [
                        new KeyboardButton("Назад")
                    ]
                ])
            {
                ResizeKeyboard = true
            };
        }


        private InlineKeyboardMarkup GetMarkup()
        {
            return new InlineKeyboardMarkup(new[]
            {
        new[]
        {
            InlineKeyboardButton.WithUrl("Переход в школу", "https://ironprogrammer.ru/#rec460811109"),
            InlineKeyboardButton.WithCallbackData("Назад", "/back {StartPage}")
        }
    });
        }
    }
}
