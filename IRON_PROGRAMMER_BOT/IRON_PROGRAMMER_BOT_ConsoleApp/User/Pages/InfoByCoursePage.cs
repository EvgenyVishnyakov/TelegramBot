using System;
using IRON_PROGRAMMER_BOT_ConsoleApp.Services;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class InfoByCoursePage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            try
            {
                var text = Resources.InfoByCoursePageText;
                var replyMarkup = GetKeyboard();
                userState.AddPage(this);

                var path = "Resources//Photos//Логотип.png";
                var resource = ResourcesService.GetResource(path);

                return new PhotoPageResult(resource, text, replyMarkup)
                {
                    UpdatedUserState = userState
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе View, файл InfoByCoursePage");
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

                if (update.CallbackQuery.Data == "Назад")
                {
                    userState.Pages.Pop();
                    return userState.CurrenntPage.View(update, userState);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе Handle, файл InfoByCoursePage");
                return View(update, userState);
            }
            return View(update, userState);
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            try
            {
                var button1 = InlineKeyboardButton.WithUrl("Переход в школу", "https://ironprogrammer.ru/#rec460811109");
                var button2 = InlineKeyboardButton.WithCallbackData("Назад", "Назад");
                return new InlineKeyboardMarkup(new[]
        {
        new[] { button1 },
        new[] { button2 }
        });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе GetKeyboard, файл InfoByCoursePage");
                return null;
            }
        }
    }
}
