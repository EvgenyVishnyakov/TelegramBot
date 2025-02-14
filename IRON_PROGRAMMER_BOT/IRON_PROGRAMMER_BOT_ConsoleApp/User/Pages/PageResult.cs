using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class PageResult
    {
        public string Text { get; }

        public ReplyMarkupBase ReplyMarkup { get; }

        public UserState UpdatedUserState { get; set; }

        public PageResult(string text, ReplyMarkupBase replyMarkup)
        {
            Text = text;
            ReplyMarkup = replyMarkup;
        }

    }
}