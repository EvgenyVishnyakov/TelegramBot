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

        public PageResultBase Handle(Update update, UserState userState)
        {
            if (update.Message != null)
            {
                userState.UserData.UserQuastion = update.Message.Text;//для дальнейшей передачи в ИИ
            }
            if (update.CallbackQuery == null)
                return new PageResultBase("Выберите действие с помощью кнопок", GetKeyboard());
            if (update.CallbackQuery.Data == "Назад")
            {
                userState.Pages.Pop();
                return userState.CurrenntPage.View(update, userState);
            }
            return null;
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            var button1 = InlineKeyboardButton.WithCallbackData("Назад", "Назад");
            return new InlineKeyboardMarkup(new[]
    {
        new[] { button1 }
        });
        }
    }
}