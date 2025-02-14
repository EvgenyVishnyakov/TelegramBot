using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class InfoByCoursePage : IPage
    {
        public PageResult View(Update update, UserState userState)
        {
            var text = @"Информация о курсах!
Вы можете перейти на страницу школы IRON PROGRAMMER";

            var replyMarkup = GetReplyKeyboard();

            return new PageResult(text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };
        }

        public PageResult Handle(Update update, UserState userState)
        {
            if (update.Message == null)
                return new PageResult("Выберите действие с помощью кнопок", GetReplyKeyboard());
            if (update.Message.Text == "Перейти на страницу школы IRON PROGRAMMER")
            {
                return new HelpByCoursePage().View(update, userState);
            }
            return null;
        }

        private ReplyKeyboardMarkup GetReplyKeyboard()
        {
            return new ReplyKeyboardMarkup(
                [
                    [
                    new KeyboardButton("Перейти на страницу школы IRON PROGRAMMER")

                    ],
                    [
                        new KeyboardButton("Назад")
                    ]
                ])
            {
                ResizeKeyboard = true
            };
        }
    }
}
//{
//    "chat_id" : MENTION_USER_CHAT_ID *,
//    "text" : "Click to Open URL",
//    "parse_mode" : "markdown",
//    "reply_markup" : {
//        "inline_keyboard" : [
//            [
//                {
//            "text" : "Open link",
//"url" : "http://example.com"
//                }
//            ]
//        ]
//   }
//}