using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class StartPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"<b>Привет!
Рад видеть тебя😊</b>

<u>Задай свой вопрос и я тебе обязательно отвечу!</u>

Хочешь получить помощь по курсу?
Хочешь узнать о нашей школе и курсах или получить консультацию от наших специалистов?
Нажми одну из <em>кнопок</em> ниже, выбирай направление - я отвечу и помогу тебе😉";

            var replyMarkup = GetKeyboard();
            userState.AddPage(this);

            return new PageResultBase(text, replyMarkup)
            {
                UpdatedUserState = userState
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            if (update.Message != null)
            {
                return View(update, userState);
            }
            if (update.CallbackQuery == null)
                return new PageResultBase("Выберите действие с помощью кнопок", GetKeyboard());
            if (update.CallbackQuery.Data == "HelpByCoursePage")
            {
                return new HelpByCoursePage().View(update, userState);
            }

            if (update.CallbackQuery.Data == "InfoByCoursePage")
            {
                return new InfoByCoursePage().View(update, userState);
            }

            if (update.CallbackQuery.Data == "ConnectWithManagerPage")
            {
                return new ConnectWithManagerPage().View(update, userState);
            }

            return null;
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            var button1 = InlineKeyboardButton.WithCallbackData("Нужна помощь по курсу", "HelpByCoursePage");
            var button2 = InlineKeyboardButton.WithCallbackData("Узнать о курсах", "InfoByCoursePage");
            var button3 = InlineKeyboardButton.WithCallbackData("Позвать менеджера", "ConnectWithManagerPage");

            return new InlineKeyboardMarkup(new[]
    {
        new[] { button1 },
        new[] { button2, button3 }
        });
        }
    }
}
