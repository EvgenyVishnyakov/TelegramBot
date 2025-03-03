using IRON_PROGRAMMER_BOT_Common.Feedback;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.Base;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class EvolutionPage(IServiceProvider services, ResourcesService resourcesService, ITelegramService telegramService, ITelegramBotClient client) : MessagePhotoPageBase(resourcesService, telegramService)
    {
        public override byte[] GetPhoto()
        {
            return Resources.Evolution;
        }

        public override string GetText(UserState userState)
        {
            return Resources.CommonTutorText;
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

            var listCoursesAndTutors = FeedbackStorage.Tutors;
            var course = listCoursesAndTutors["EvolutionPage"];
            var randomIndex = random.Next(course.Count);
            var managerChatId = course.ElementAt(randomIndex);

            Task task = SendMessageRequestAsync(managerChatId, userName, userFirstName, userMessage);

            userState.requestCounter = 0;
            return userState;
        }

        private async Task SendMessageRequestAsync(long managerChatId, string? userName, string? userFirstName, string? userMessage)
        {
            if (userName == string.Empty)
            {
                await client.SendTextMessageAsync(
                    chatId: managerChatId,
                    text: $"Студент {userFirstName} просит в курсе Эволюция языка ответить на следующий вопрос:{Environment.NewLine}{userMessage}",
                    parseMode: ParseMode.MarkdownV2);
            }
            else
                await client.SendTextMessageAsync(
                    chatId: managerChatId,
                    $"Пользователь [{userFirstName}](http://t\\.me/{userName}) в курсе Эволюция языка прислал сообщение{Environment.NewLine}{userMessage}",
                    parseMode: ParseMode.MarkdownV2);
        }

        public override IPage GetNextPage()
        {
            return services.GetRequiredService<BackwardDummyPage>();
        }
    }
}