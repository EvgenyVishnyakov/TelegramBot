using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class HelpByCoursePage : IPage
    {

        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Онлайн консультация!

Получение ответа на вопрос
Ты находишься в разделе получения информации по интересующему вопросу

В этом разделе помогает мощный интелект всемирной паутины😊

Выбери , пожалуйста, какого формата у тебя вопрос и тебе обязательно помогут.

Совет: спрашивай общее направление, пытайся до глубины задачи дойти сам!
Успехов!";
            var videoUrl = "https://drive.google.com/uc?export=download&id=1zLn8xYgNdAcvBH9P9sHRpsvzZYi1hETJ";
            var replyMarkup = GetReplyKeyboard();

            return new VideoPageResult(InputFile.FromUri(videoUrl), text, replyMarkup)
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
                return new StartPage().View(update, userState);
            }
            if (update.Message.Text == "Общий вопрос по изучаемой теме")
            {
                return new CommonQuestionsPage().View(update, userState);
            }
            if (update.Message.Text == "Вопрос по конкретной задаче")
            {
                return new ResolveTaskPage().View(update, userState);
            }

            return null;
        }

        private ReplyKeyboardMarkup GetReplyKeyboard()
        {
            return new ReplyKeyboardMarkup(
                [
                    [
                        new KeyboardButton("Общий вопрос по изучаемой теме")
                    ],
                    [
                        new KeyboardButton("Вопрос по конкретной задаче")
                    ],
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