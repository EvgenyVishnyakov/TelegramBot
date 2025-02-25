using IRON_PROGRAMMER_BOT_Common.User.Pages.Base;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class StartPage(IServiceProvider services) : CallbackQueryPageBase
    {
        public override string GetText(UserState userState)
        {
            return Resources.StartPageText;
        }

        public override ButtonLinkPage[][] GetKeyBoardAsync()
        {
            try
            {
                return [
                    [
                     new ButtonLinkPage(InlineKeyboardButton.WithCallbackData("Нужна помощь по курсу",Resources.HelpByCoursePage), services.GetRequiredService<HelpByCoursePage>())
                        ],
                        [
                    new ButtonLinkPage(InlineKeyboardButton.WithCallbackData("Узнать о курсах", Resources.InfoByCoursePage), services.GetRequiredService<InfoByCoursePage>()),
                    new ButtonLinkPage(InlineKeyboardButton.WithCallbackData("Позвать менеджера", Resources.ConnectWithManagerPage), services.GetRequiredService<ConnectWithManagerPage>())
                            ]
                    ];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе GetKeyboard, файл StartPage");
                return null;
            }
        }
    }
}
