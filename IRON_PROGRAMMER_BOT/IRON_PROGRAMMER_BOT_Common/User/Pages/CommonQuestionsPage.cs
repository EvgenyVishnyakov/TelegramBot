using IRON_PROGRAMMER_BOT_Common.GigaChatApi;
using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.User.Pages.Base;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class CommonQuestionsPage : MessagePageBase
    {
        private string text1 { get; set; }
        private readonly IServiceProvider _services;
        private readonly IGigaChatApiProvider _gigaChatApiProvider;

        public CommonQuestionsPage(IServiceProvider services, IGigaChatApiProvider gigaChatApiProvider) : base()
        {
            _services = services;
            _gigaChatApiProvider = gigaChatApiProvider;
        }

        //public override byte[] GetPhoto()
        //{
        //    return Resources.Фото_ИИ;
        //}

        public override string GetText(UserState userState)
        {
            var text = Resources.CommonQuestionsPageText;
            return $"{text}{Environment.NewLine}{Environment.NewLine}{text1}";
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
            Completion completion = new Completion();
            var auth = _gigaChatApiProvider.EnsureAuthenticatedAsync().Result;
            var counter = 3;
            //var auth = _gigaChatApiProvider.AuthenticateAsync().Result;
            //userState.UserData.UserQuestion = message.Text;
            var inputTask = $"Ты главный специалист по C# в мире. Ответь на вопрос четко, структурированно, но кратко, укладываясь в 600 символов.{Environment.NewLine}";
            var prompt = inputTask + message.Text;// реализовать поход в ИИ
            CompletionSettings settings = new CompletionSettings("GigaChat:latest", 1f, null, 4);
            var result = completion.SendRequest(auth.GigaChatAuthorizationResponse?.AccessToken!, prompt).Result;//response.GigaChatAuthorizationResponse!.AccessToken
            if (counter == 0)
                return (UserState)GetNextPage();
            if (result.RequestSuccessed)
            {
                foreach (var it in result.GigaChatCompletionResponse!.Choices)
                {
                    text1 += $"{it.Message.Content}{Environment.NewLine}";
                    counter--;
                    Console.WriteLine(it.Message.Content);
                }
            }
            else
            {
                Console.WriteLine(result.ErrorTextIfFailed);
            }
            return userState;
        }

        public override IPage GetNextPage()
        {
            return _services.GetRequiredService<BackwardDummyPage>();//страница получения ответа на вопрос
        }
    }
}