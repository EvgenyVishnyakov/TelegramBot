using IRON_PROGRAMMER_BOT_ConsoleApp.Services;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class HelpByCoursePage : IPage
    {

        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"<b><u>Онлайн консультация!</u></b>

Получение ответа на вопрос
Ты находишься в разделе получения информации по интересующему вопросу

<u><i>В этом разделе помогает мощный интелект всемирной паутины😊</i></u>

Выбери , пожалуйста, какого <em>формата</em> у тебя вопрос и тебе обязательно помогут.

<b>Совет: спрашивай общее направление, пытайся до глубины задачи дойти сам!</b>❗️
Успехов!";
            var path = "Resources\\Videos\\ИИ.mp4";
            var replyMarkup = GetKeyboard();
            var resource = ResourcesService.GetResource(path);
            return new VideoPageResult(resource, text, replyMarkup)
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
            if (update.CallbackQuery.Data == "CommonQuestionsPage")
            {
                return new CommonQuestionsPage().View(update, userState);
            }
            if (update.CallbackQuery.Data == "ResolveTaskPage")
            {
                return new ResolveTaskPage().View(update, userState);
            }

            return null;
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            var button1 = InlineKeyboardButton.WithCallbackData("Общий вопрос по изучаемой теме", "CommonQuestionsPage");
            var button2 = InlineKeyboardButton.WithCallbackData("Вопрос по конкретной задаче", "ResolveTaskPage");
            var button3 = InlineKeyboardButton.WithCallbackData("Назад", "Назад");

            return new InlineKeyboardMarkup(new[]
    {
        new[] { button1 },
        new[] { button2 },
        new[] {button3 }
        });
        }
    }
}