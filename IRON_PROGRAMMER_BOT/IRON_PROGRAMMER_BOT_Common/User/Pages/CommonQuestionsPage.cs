using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.User.Pages.Base;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class CommonQuestionsPage(IServiceProvider services, ResourcesService resourcesService, ITelegramBotClient client) : MessagePhotoPageBase(resourcesService, client)
    {
        public override byte[] GetPhoto()
        {
            return Resources.Фото_ИИ;
        }

        public override string GetText(UserState userState)
        {
            return Resources.CommonQuestionsPageText;
        }

        public override ButtonLinkPage[][] GetKeyBoard()
        {
            return [
                [
                        new ButtonLinkPage(InlineKeyboardButton.WithCallbackData(Resources.Back),services.GetRequiredService<BackwardDummyPage>())
                        ]
                ];
        }

        public override UserState ProcessMessage(Message message, UserState userState)
        {
            userState.UserData.StepiId = message.Text;// реализовать поход в ИИ
            return userState;
        }

        public override IPage GetNextPage()
        {
            return services.GetRequiredService<BackwardDummyPage>();//страница получения ответа на вопрос
        }
    }
}