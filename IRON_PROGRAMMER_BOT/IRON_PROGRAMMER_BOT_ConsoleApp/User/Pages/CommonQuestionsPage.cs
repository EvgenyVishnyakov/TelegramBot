using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class CommonQuestionsPage : IPage
    {
        public PageResult View(Update update, UserState userState)
        {
            var text = @"Задавайте свой вопрос";

            var replyMarkup = GetReplyKeyboard();

            return new PageResult(text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };
        }

        public PageResult Handle(Update update, UserState userState)
        {
            if (update.Message == null)
                return new PageResult("Выберите действие с помощью кнопок", GetReplyKeyboard());
            if (update.Message.Text == "Назад")
            {
                return new HelpByCoursePage().View(update, userState);
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
    }
}