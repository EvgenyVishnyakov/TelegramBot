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
            if (update.CallbackQuery == null)
                return View(update, userState);
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

            return View(update, userState);
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе GetKeyboard, файл StartPage");
                return null;
            }
        }
    }
}
