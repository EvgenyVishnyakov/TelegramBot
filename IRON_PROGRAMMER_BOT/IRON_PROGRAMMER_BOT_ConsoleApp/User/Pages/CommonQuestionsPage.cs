using System;
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
            try
            {
                var text = Resources.CommonQuestionsPageText;

                var path = "Resources//Photos//Фото ИИ.jpg";

                var resource = ResourcesService.GetResource(path);
                var replyMarkup = GetKeyboard();
                userState.AddPage(this);

                return new PhotoPageResult(resource, text, replyMarkup)
                {
                    UpdatedUserState = userState
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе View, файл CommonQuestionsPage");
                return View(update, userState);
            }
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            try
            {
                if (update.Message != null)
                {
                    userState.UserData.UserQuastion = update.Message.Text;//для дальнейшей передачи в ИИ
                }
                if (update.CallbackQuery == null)
                    return new PageResultBase("Выберите действие с помощью кнопок", GetKeyboard());
                if (update.CallbackQuery.Data == Resources.Back)
                {
                    userState.Pages.Pop();
                    return userState.CurrenntPage.View(update, userState);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе Handle, файл CommonQuestionsPage");
                return View(update, userState);
            }
            return View(update, userState);
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            try
            {
                var back = InlineKeyboardButton.WithCallbackData(Resources.Back);
                return new InlineKeyboardMarkup(new[]
        {
        new[] { back }
        });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе GetKeyboard, файл CommonQuestionsPage");
                return null;
            }
        }
    }
}