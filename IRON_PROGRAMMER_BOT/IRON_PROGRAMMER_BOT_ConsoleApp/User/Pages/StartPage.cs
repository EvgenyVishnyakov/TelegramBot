using System;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class StartPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            try
            {
                var text = Resources.StartPageText;

                var replyMarkup = GetKeyboard();
                userState.AddPage(this);

                return new PageResultBase(text, replyMarkup)
                {
                    UpdatedUserState = userState
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе View, файл StartPage");
                return View(update, userState);
            }
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            try
            {
                if (update.Message != null)
                {
                    return View(update, userState);
                }
                if (update.CallbackQuery == null)
                    return new PageResultBase("Выберите действие с помощью кнопок", GetKeyboard());
                if (update.CallbackQuery.Data == Resources.HelpByCoursePage)
                {
                    return new HelpByCoursePage().View(update, userState);
                }
                if (update.CallbackQuery.Data == Resources.InfoByCoursePage)
                {
                    return new InfoByCoursePage().View(update, userState);
                }

                if (update.CallbackQuery.Data == Resources.ConnectWithManagerPage)
                {
                    return new ConnectWithManagerPage().View(update, userState);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе View, файл Handle");
                return View(update, userState);
            }

            return View(update, userState);
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            try
            {
                var button1 = InlineKeyboardButton.WithCallbackData("Нужна помощь по курсу", Resources.HelpByCoursePage);
                var button2 = InlineKeyboardButton.WithCallbackData("Узнать о курсах", Resources.InfoByCoursePage);
                var button3 = InlineKeyboardButton.WithCallbackData("Позвать менеджера", Resources.ConnectWithManagerPage);

                return new InlineKeyboardMarkup(new[]
        {
        new[] { button1 },
        new[] { button2, button3 }
        });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе GetKeyboard, файл StartPage");
                return null;
            }
        }
    }
}
