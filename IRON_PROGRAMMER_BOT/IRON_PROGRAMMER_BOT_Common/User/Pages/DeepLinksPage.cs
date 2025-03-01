using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.User.Pages.Base;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class DeepLinksPage(IServiceProvider services, ITelegramService telegramService) : CallbackQueryPageBase(telegramService)
    {
        public override ButtonLinkPage[][] GetKeyBoardAsync()
        {
            return [
                [
                new ButtonLinkPage(InlineKeyboardButton.WithUrl("Для Продвинутых", "https://t.me/+h8cdhllBlEhiZDIy"),null)
                    ],
                    [
                        new ButtonLinkPage(InlineKeyboardButton.WithUrl("Коллекции", "https://t.me/+IzXHMt9FjOY0YmIy"),null)
                        ],
                        [
                            new ButtonLinkPage(InlineKeyboardButton.WithUrl("LINQ", "https://t.me/csharp_linq"),null)
                            ],
                            [
                                new ButtonLinkPage(InlineKeyboardButton.WithCallbackData(Resources.Back),services.GetRequiredService<BackwardDummyPage>())
                                ]
                ];
        }

        public override string GetText(UserState userState)
        {
            return "Переходи по кнопкам для ответа на вопрос";
        }
    }
}
