using System;
using IRON_PROGRAMMER_BOT_ConsoleApp.Services;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class ResolveTaskPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            try
            {
                var text = @"<b>Решение задачи! 💻</b>
<u><i>Отправьте , пожалуйста:</i></u>
<i> - ссылку на задачу</i>
<i> - ссылку на Ваше решение</i>
<i> - Ваш вопрос</i>";

                var path = "Resources//Photos//Фото ИИ.jpg";
                var replyMarkup = GetKeyboard();
                var resource = ResourcesService.GetResource(path);
                userState.AddPage(this);

                return new PhotoPageResult(resource, text, replyMarkup)
                {
                    UpdatedUserState = userState
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе View, файл ResolveTaskPage");
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
                Console.WriteLine($"Ошибка {ex} в методе Handle, файл ResolveTaskPage");
                return View(update, userState);
            }
            return View(update, userState);
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            try
            {


                var button1 = InlineKeyboardButton.WithCallbackData("Назад", "Назад");
                return new InlineKeyboardMarkup(new[]
        {
        new[] { button1 }
        });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе GetKeyboard, файл ResolveTaskPage");
                return null;
            }
        }
    }
}