using IRON_PROGRAMMER_BOT_ConsoleApp.Services;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class ConnectWithManagerPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Обращение к сотрудникам школы!
Задавайте свой вопрос 
Мы вернемся с обратной связью в ближайшее время!
Спасибо за Ваш интерес!";

            var replyMarkup = GetReplyKeyboard();
            var path = "Resourses//Photos//Обратная связь.png";
            var resource = ResourcesService.GetResource(path);
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
                return new StartPage().View(update, userState);
            }
            if (update.Message.Text == "Отправить вопрос")//реализовать следующий переход к распределению вопроса
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
                        new KeyboardButton("Отправить вопрос")
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