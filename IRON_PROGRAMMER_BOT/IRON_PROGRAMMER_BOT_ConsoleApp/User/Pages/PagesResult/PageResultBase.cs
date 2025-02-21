using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult
{
    public class PageResultBase
    {
        public string Text { get; }

        public IReplyMarkup ReplyMarkup { get; }

        public ParseMode ParseMode { get; } = ParseMode.Html;

        public UserState UpdatedUserState { get; set; }

        public PageResultBase(string text, IReplyMarkup replyMarkup)
        {
            Text = text;
            ReplyMarkup = replyMarkup;
        }

        public bool IsMedia => this is PhotoPageResult ||
                                 this is VideoPageResult ||
                                 this is AudioPageResult ||
                                 this is DocumentPageResult;

    }
}

