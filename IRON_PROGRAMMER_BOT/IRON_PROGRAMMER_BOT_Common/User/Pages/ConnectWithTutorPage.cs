using IRON_PROGRAMMER_BOT_Common.CoursesPage;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.Base;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types.ReplyMarkups;
namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class ConnectWithTutorPage(IServiceProvider services, ResourcesService resourcesService, ITelegramService telegramService) : CallbackQueryPhotoPageBase(resourcesService, telegramService)
    {

        public override byte[] GetPhoto()
        {
            return Resources.Help;
        }

        public override string GetText(UserState userState)
        {
            return Resources.ConnectWithTutor;
        }

        public override ButtonLinkPage[][] GetKeyBoardAsync()
        {
            return [
                [
               new ButtonLinkPage(InlineKeyboardButton.WithCallbackData("Основы программирования", Resources.BasicsProgrammingPage), services.GetRequiredService<BasicsProgrammingPage>())
                    ],
                    [
                new ButtonLinkPage(InlineKeyboardButton.WithCallbackData("Для продвинутых", Resources.AdvancedPage), services.GetRequiredService<AdvancedPage>())
                    ],
                    [
                new ButtonLinkPage(InlineKeyboardButton.WithCallbackData("LINQ", Resources.LINQPage), services.GetRequiredService<LINQPage>())
                    ],
                    [
                new ButtonLinkPage(InlineKeyboardButton.WithCallbackData("Коллекции", Resources.CollectionPage), services.GetRequiredService<CollectionPage>())
                    ],
                    [
                new ButtonLinkPage(InlineKeyboardButton.WithCallbackData("Алгоритмы поиска и сортировки", Resources.AlgorithmPage), services.GetRequiredService<AlgorithmPage>())
                    ],
                    [
                new ButtonLinkPage(InlineKeyboardButton.WithCallbackData("Чистый код", Resources.CodePage), services.GetRequiredService<CodePage>())
                    ],
                    [
                new ButtonLinkPage(InlineKeyboardButton.WithCallbackData("Эволюция языка", Resources.EvolutionPage), services.GetRequiredService<EvolutionPage>())
                    ],
                [
                     new ButtonLinkPage(InlineKeyboardButton.WithCallbackData(Resources.Back),services.GetRequiredService<BackwardDummyPage>())
                        ]
                ];
        }




    }
}