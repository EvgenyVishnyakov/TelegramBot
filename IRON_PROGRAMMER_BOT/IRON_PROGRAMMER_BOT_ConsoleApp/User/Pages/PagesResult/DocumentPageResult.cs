﻿using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult
{
    public class DocumentPageResult : PageResultBase
    {
        public InputFile Document { get; set; }

        public DocumentPageResult(InputFile document, string text, IReplyMarkup replyMarkup) : base(text, replyMarkup)
        {
            Document = document;
        }
    }
}

