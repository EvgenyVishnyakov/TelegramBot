using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class NotStatedPage(IServiceProvider services) : IPage
    {
        public PageResultBase Handle(Update update, UserState userState)
        {
            return services.GetRequiredService<StartPage>().View(update, userState);
        }

        public PageResultBase View(Update update, UserState userState)
        {
            return services.GetRequiredService<StartPage>().View(update, userState);
        }
    }
}
