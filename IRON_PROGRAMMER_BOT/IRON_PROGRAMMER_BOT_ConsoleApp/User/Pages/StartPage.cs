﻿using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class StartPage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            var text = @"Привет!
Рад видеть тебя😊

Задай свой вопрос и я тебе обязательно отвечу!

Хочешь получить помощь по курсу?
Хочешь узнать о курсах или получить консультацию от наших специалистов?
Нажми одну из кнопок ниже, выбирай направление - я отвечу и помогу тебе😉";

            var replyMarkup = GetReplyKeyboard();

            return new PageResultBase(text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            if (update.Message == null)
                return new PageResultBase("Выберите действие с помощью кнопок", GetReplyKeyboard());
            if (update.Message.Text == "Нужна помощь по курсу")
            {
                return new HelpByCoursePage().View(update, userState);
            }

            if (update.Message.Text == "Узнать о курсах")
            {
                return new InfoByCoursePage().View(update, userState);
            }

            if (update.Message.Text == "Позвать менеджера")
            {
                return new ConnectWithManagerPage().View(update, userState);
            }

            return null;
        }

        private ReplyKeyboardMarkup GetReplyKeyboard()
        {
            return new ReplyKeyboardMarkup(
                [
                    [
                        new KeyboardButton("Нужна помощь по курсу")
                    ],
                    [
                        new KeyboardButton("Узнать о курсах")
                    ],
                    [
                        new KeyboardButton("Позвать менеджера")
                    ]
                ])
            {
                ResizeKeyboard = true
            };
        }
    }
}
