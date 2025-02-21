using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult
{
    public class VideoPageResult : PageResultBase
    {
        public InputFile Video { get; set; }
        public ParseMode? ParseMode { get; set; }

        public VideoPageResult(InputFile video, string text, IReplyMarkup replyMarkup) : base(text, replyMarkup)
        {
            Video = video;
        }
    }
}

