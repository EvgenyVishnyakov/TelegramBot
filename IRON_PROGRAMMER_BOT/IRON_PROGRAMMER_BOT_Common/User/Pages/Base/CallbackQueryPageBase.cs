using IRON_PROGRAMMER_BOT_Common.Interfaces;
using IRON_PROGRAMMER_BOT_Common.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages.Base
{
    public abstract class CallbackQueryPageBase(ITelegramService telegramService) : IPage
    {
        public abstract string GetText(UserState userState);
        public abstract ButtonLinkPage[][] GetKeyBoardAsync();

        public virtual PageResultBase View(Update update, UserState userState)
        {
            try
            {
                if (update?.CallbackQuery != null || update?.Message?.Text != null)
                    telegramService.SendChatTypingActionAsync(update).GetAwaiter();
                var text = GetText(userState);
                var replyMarkup = GetInlineKeyboardMarkup();
                userState.AddPage(this);
                return new PageResultBase(text, replyMarkup)
                {
                    UpdatedUserState = userState
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе View, файл StartPage");
                return View(update, userState);
            }
        }

        public virtual PageResultBase Handle(Update update, UserState userState)
        {
            try
            {
                if (update.Message != null)
                {
                    return View(update, userState);
                }

                var buttons = GetKeyBoardAsync().SelectMany(x => x);
                var pressedButton = buttons.FirstOrDefault(x => x.Button.CallbackData == update.CallbackQuery.Data);
                return pressedButton!.Page.View(update, userState);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе View, файл Handle");
                return View(update, userState);
            }
        }

        protected InlineKeyboardMarkup GetInlineKeyboardMarkup()
        {
            return new InlineKeyboardMarkup(GetKeyBoardAsync().Select(page => page.Select(x => x.Button)));
        }
    }
}
