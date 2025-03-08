using IRON_PROGRAMMER_BOT_Common.GigaChatApi;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.User.Pages.Base;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class CommonQuestionsPage(IServiceProvider services, IGigaChatApiProvider gigaChatApiProvider, ITelegramService telegramService) : MessagePageBase(telegramService)
    {
        int attemptCounter = 3;
        private string answerAI { get; set; }
        private readonly IServiceProvider _services = services;
        private readonly IGigaChatApiProvider _gigaChatApiProvider = gigaChatApiProvider;

        public override string GetText(UserState userState)
        {
            try
            {
                var text = Resources.CommonQuestionsPageText;
                if (attemptCounter < 0)
                    return answerAI = Resources.CoomQuestionPageStopAI;
                if (attemptCounter == 0)
                    return $"{text}{Environment.NewLine}{Environment.NewLine}{Resources.CommonQuestionPageFinalTrying}{Environment.NewLine}{Environment.NewLine}{answerAI}";
                if (attemptCounter == 1)
                    return $"{text}{Environment.NewLine}{Environment.NewLine}{Resources.CommonQuestionPagePenultimateQuestion}{Environment.NewLine}{answerAI}";

                return $"{text}{Environment.NewLine}{Environment.NewLine}**У тебя есть возможность задать **__{attemptCounter}__** вопроса!**{Environment.NewLine}{Environment.NewLine}{answerAI}";
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }

        public override ButtonLinkPage[][] GetKeyBoardAsync()
        {
            return [
                [
                        new ButtonLinkPage(InlineKeyboardButton.WithCallbackData(Resources.Back),_services.GetRequiredService<BackwardDummyPage>())
                        ]
                ];
        }

        public override UserState ProcessMessageAsync(Message message, UserState userState)
        {
            try
            {
                var completion = new Completion();
                var auth = _gigaChatApiProvider.EnsureAuthenticatedAsync().Result;
                var prompt = Resources.HeaderPromtForAI + Environment.NewLine + message.Text;

                var settings = new CompletionSettings();
                var result = completion.SendRequest(auth.GigaChatAuthorizationResponse?.AccessToken!, prompt).Result;

                if (result.RequestSuccessed)
                {
                    foreach (var it in result.GigaChatCompletionResponse!.Choices!)
                    {
                        answerAI += $"{it.Message!.Content}{Environment.NewLine}";
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
                Log.Error($"Ошибка {ex.ToString()} в методе ProcessMessageAsync в классе CommonQuestionsPage");
                return userState;
            }
        }

        public override IPage GetNextPage()
        {
            return _services.GetRequiredService<BackwardDummyPage>();
        }
    }
}