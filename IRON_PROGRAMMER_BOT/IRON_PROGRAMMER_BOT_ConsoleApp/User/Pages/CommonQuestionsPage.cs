using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class CommonQuestionsPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Задавайте свой вопрос";
            var photoUrl = "https://drive.google.com/uc?export=download&id=1uCk5Z4o-PFPM1Pv1oike9YX5GwR7r4vA";

            var replyMarkup = GetReplyKeyboard();

            return new PhotoPageResult(InputFile.FromUri(photoUrl), text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            if (update.Message == null)
                return new PageResultBase("Выберите действие с помощью кнопок", GetReplyKeyboard());
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