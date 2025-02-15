using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class ConnectWithManagerPage : IPage
    {
        public PageResult View(Update update, UserState userState)
        {
            var text = @"Обращение к сотрудникам школы!
Задавайте свой вопрос 
Мы вернемся с обратной связью в ближайшее время!
Спасибо за Ваш интерес!";

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