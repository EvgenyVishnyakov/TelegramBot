using IRON_PROGRAMMER_BOT_Common.GigaChatApi;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.User.Pages.Base;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class ResolveTaskPage(IServiceProvider services, IGigaChatApiProvider gigaChatApiProvider, ITelegramService telegramService) : MessagePageBase(telegramService)
    {
        int attemptCounter = 1;
        private string answerAI { get; set; }
        private readonly IServiceProvider _services = services;
        private readonly IGigaChatApiProvider _gigaChatApiProvider = gigaChatApiProvider;

        public override string GetText(UserState userState)
        {
            try
            {
                var text = Resources.ResolveTaskPageText;
                if (attemptCounter < 0)
                    return answerAI = Resources.CoomQuestionPageStopAI;
                return $"{text}{Environment.NewLine}{Environment.NewLine}{answerAI}";
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка {ex.ToString()} в методе GetText в классе ResolveTaskPage");
                return null;
            }
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
                var completion = new Completion();
                var auth = _gigaChatApiProvider.EnsureAuthenticatedAsync().Result;
                var prompt = Resources.TaskPromt + Environment.NewLine + message.Text;

                var settings = new CompletionSettings("GigaChat:latest", 0.8f, null, 4);
                var result = completion.SendRequest(auth.GigaChatAuthorizationResponse?.AccessToken!, prompt).Result;

                if (result.RequestSuccessed)
                {
                    foreach (var it in result.GigaChatCompletionResponse!.Choices!)
                    {
                        answerAI += $"{it.Message!.Content}";

                        userState.requestCounter = attemptCounter--;
                    }
                }
                else
                {
                    Console.WriteLine(result.ErrorTextIfFailed);
                }
                return userState;
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка {ex.ToString()} в методе ProcessMessageAsync в классе ResolveTaskPage");
                return userState;
            }
        }

        public override IPage GetNextPage()
        {
            return _services.GetRequiredService<BackwardDummyPage>();
        }
    }
}