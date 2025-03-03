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
    public class PasswordPage(IServiceProvider services, ResourcesService resourcesService, ITelegramService telegramService, ITelegramBotClient client) : MessagePhotoPasswordPageBase(resourcesService, telegramService)
    {
        public override byte[] GetPhoto()
        {
            return Resources.Password;
        }

        public override string GetText(UserState userState)
        {
            return Resources.PasswordPageText;
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
            var userMessage = message.Text;
            var userChatId = message.Chat.Id;
            var password = Environment.GetEnvironmentVariable("TuterPassword")!;
            if (!IsPassword(userMessage, password))
            {
                Task task = SendMessageRequestAsync(userChatId);
                userState.IsPassword = false;
                return userState;
            }
            userState.IsPassword = true;
            return userState;
        }

        private bool IsPassword(string? userMessage, string password)
        {
            return userMessage == password;
        }

        private async Task SendMessageRequestAsync(long chatId)
        {
            await client.SendTextMessageAsync(
                     chatId: chatId,
                     text: $"Введен неверный пароль! Вы были перенаправлены на начальную страницу!",
                     parseMode: ParseMode.Html);
        }

        public override IPage GetNextPage()
        {
            return services.GetRequiredService<DeepLinksPage>();
        }

        public override IPage GetStartPage()
        {
            return services.GetRequiredService<BackwardDummyPage>();
        }
    }
}