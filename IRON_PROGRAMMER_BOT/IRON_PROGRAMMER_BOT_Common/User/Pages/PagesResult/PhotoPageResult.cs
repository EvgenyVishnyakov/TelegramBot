﻿using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult
{
    public class PageResultBas : PageResultBase
    {
        public InputFile Photo { get; set; }
        public ParseMode? ParseMode { get; set; }

        public PageResultBas(InputFile photo, string text, IReplyMarkup replyMarkup) : base(text, replyMarkup)
        {
            Photo = photo;
        }
    }
}

