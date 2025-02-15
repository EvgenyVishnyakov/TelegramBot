using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class PageResultBase
    {
        public string Text { get; }

        public IReplyMarkup ReplyMarkup { get; }

        public UserState UpdatedUserState { get; set; }

        public PageResultBase(string text, IReplyMarkup replyMarkup)
        {
            Text = text;
            ReplyMarkup = replyMarkup;
        }
    }
}

