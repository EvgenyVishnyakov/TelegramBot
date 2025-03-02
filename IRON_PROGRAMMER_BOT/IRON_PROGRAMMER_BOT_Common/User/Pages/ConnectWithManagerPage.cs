using IRON_PROGRAMMER_BOT_Common.Feedback;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.Base;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class ConnectWithManagerPage(IServiceProvider services, ResourcesService resourcesService, ITelegramService telegramService) : MessagePhotoPageBase(resourcesService, telegramService)
    {
        public override byte[] GetPhoto()
        {
            return Resources.FeedBack;
        }

        public override string GetText(UserState userState)
        {
            return Resources.ConnectWithManagerPageText;
        }

        public override ButtonLinkPage[][] GetKeyBoardAsync()
        {
            return [
                [
                     new ButtonLinkPage(InlineKeyboardButton.WithCallbackData(Resources.Back),services.GetRequiredService<BackwardDummyPage>())
                        ]
                ];
        }

        public override UserState ProcessMessageAsync(Message message, UserState userState)
        {
            Random random = new Random();
            var userMessage = message.Text;// реализовать страницу с вопросом к кураторам
            var managers = FeedbackStorage.GetManagers();
            var randomIndex = random.Next(managers.Count);
            var chosenManager = managers[randomIndex];


            return userState;
        }

        public override IPage GetNextPage()
        {
            return services.GetRequiredService<BackwardDummyPage>();
        }
    }
}
