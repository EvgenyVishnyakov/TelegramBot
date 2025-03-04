using IRON_PROGRAMMER_BOT_Common.Feedback;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.Base;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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
            try
            {
                Random random = new Random();

                var userMessage = message.Text;
                var userName = message.From?.Username;
                var userFirstName = message.From!.FirstName;
                var userChatId = message.Chat.Id;

                var managers = FeedbackStorage.GetManagers();
                var randomIndex = random.Next(managers.Count);

                var chosenManager = managers.ElementAt(randomIndex);
                var managerUserName = chosenManager.Key;
                var managerDate = chosenManager.Value.FirstOrDefault();
                var managerName = managerDate.Item1;
                var managerChatId = managerDate.Item2;

                Task task = SendMessageRequestAsync(managerChatId, managerUserName, managerName, userName, userFirstName, userMessage, userChatId);

                userState.requestCounter = 0;
                return userState;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Ошибка {e.ToString()} в методе ProcessMessageAsync на странице ConnectWithManagerPage");
                return userState;
            }
        }

        private async Task SendMessageRequestAsync(long managerChatId, string? managerUserName, string managerName, string? userName, string? userFirstName, string? userMessage, long userChatId)
        {
            try
            {
                if (userName == string.Empty)
                {
                    await client.SendTextMessageAsync(
                        chatId: userChatId,
                        text: $"Просьба прислать ваш username для связи с Вами, так как действующего username телеграмма у Вас нет либо можете написать напрямую нашему менеджеру:[{managerName}](http://t\\.me/{managerUserName})",
                        parseMode: ParseMode.MarkdownV2);
                }
                else
                    await client.SendTextMessageAsync(
                        chatId: managerChatId,
                        $"Пользователь [{userFirstName}](http://t\\.me/{userName}) прислал сообщение{Environment.NewLine}{userMessage}",
                        parseMode: ParseMode.MarkdownV2);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}", ex);
            }

        }

        public override IPage GetNextPage()
        {
            return services.GetRequiredService<BackwardDummyPage>();
        }
    }
}
