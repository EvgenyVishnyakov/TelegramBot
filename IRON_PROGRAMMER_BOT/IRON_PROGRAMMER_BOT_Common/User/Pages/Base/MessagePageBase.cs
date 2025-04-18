﻿using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Serilog;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages.Base
{
    public abstract class MessagePageBase(ITelegramService telegramService) : CallbackQueryPageBase(telegramService)
    {
        public abstract UserState ProcessMessageAsync(Message message, UserState userState);

        public abstract IPage GetNextPage();

        public override PageResultBase Handle(Update update, UserState userState)
        {
            try
            {
                if (update.Message == null)
                    return base.Handle(update, userState);
                var updateUserState = ProcessMessageAsync(update.Message, userState);
                if (updateUserState.requestCounter > 0)
                    return base.Handle(update, userState);
                var nextPage = GetNextPage();
                return nextPage.View(update, updateUserState);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return base.Handle(update, userState);
            }
        }
    }
}
