using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class ConnectWithManagerPage(ResourcesService resourcesServise, IServiceProvider services) : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            try
            {
                var text = Resources.ConnectWithManagerPageText;
                var replyMarkup = GetKeyboard();
                var path = Resources.Обратная_связь;
                var resource = resourcesServise.GetResource(path, "Обратная связь");
                userState.AddPage(this);

                return new PhotoPageResult(resource, text, replyMarkup)
                {
                    UpdatedUserState = userState
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе View, файл ConnectWithManagerPage");
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
                if (update.CallbackQuery.Data == Resources.Back)
                {
                    userState.Pages.Pop();
                    return userState.CurrenntPage.View(update, userState);
                }
                if (update.CallbackQuery.Data == Resources.SendQuastion)//реализовать следующий переход к распределению вопроса
                {
                    return services.GetRequiredService<StartPage>().View(update, userState);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе Handle, файл ConnectWithManagerPage");
                return View(update, userState);
            }
            return View(update, userState);
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            try
            {
                var sendQuastion = InlineKeyboardButton.WithCallbackData("Отправить вопрос", Resources.SendQuastion);

                var back = InlineKeyboardButton.WithCallbackData(Resources.Back);

                return new InlineKeyboardMarkup(new[]
        {
        new[] { sendQuastion, back }
        });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе GetKeyboard, файл ConnectWithManagerPage");
                return null;
            }
        }
    }
}