using IRON_PROGRAMMER_BOT_Common.Feedback;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.Base;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class ConnectWithManagerPage(IServiceProvider services, ResourcesService resourcesService, ITelegramService telegramService, ITelegramBotClient client) : MessagePhotoPageBase(resourcesService, telegramService)
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
            var userMessage = message.Text;
            var userName = message.From?.Username;
            var userFirstName = message.From!.FirstName;
            var managers = FeedbackStorage.GetManagers();
            var randomIndex = random.Next(managers.Count);
            var chosenManager = randomIndex;
            Task task = SendMessageRequestAsync(chosenManager, userName, userMessage, userFirstName);

            userState.requestCounter = 0;
            return userState;
        }

        private async Task SendMessageRequestAsync(long chosenManager, string? userName, string? userMessage, string? userFirstName)
        {
            await client.SendTextMessageAsync(chosenManager, $"Сообщение от пользователя:{userFirstName},/tg://resolve?domain={userName} {userMessage}");
        }

        public override IPage GetNextPage()
        {
            return services.GetRequiredService<BackwardDummyPage>();
        }
    }
}
