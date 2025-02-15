using IRON_PROGRAMMER_BOT_ConsoleApp.Services;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class CommonQuestionsPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Задавайте свой вопрос";

            var path = "Resourses//Photos//Фото ИИ.jpg";

            var resource = ResourcesService.GetResource(path);
            var replyMarkup = GetReplyKeyboard();

            return new PhotoPageResult(resource, text, replyMarkup)
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