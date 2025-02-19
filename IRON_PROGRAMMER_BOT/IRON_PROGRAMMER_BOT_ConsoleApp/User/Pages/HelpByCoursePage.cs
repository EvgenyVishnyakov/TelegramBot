﻿using System;
using IRON_PROGRAMMER_BOT_ConsoleApp.Services;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages.PagesResult;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages
{
    public class HelpByCoursePage : IPage
    {
        public PageResultBase View(Update update, UserState userState)
        {
            try
            {
                var text = Resources.HelpByCoursePageText;
                var path = "Resources\\Videos\\ИИ.mp4";
                var replyMarkup = GetKeyboard();
                var resource = ResourcesService.GetResource(path);
                userState.AddPage(this);

                return new VideoPageResult(resource, text, replyMarkup)
                {
                    UpdatedUserState = userState
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе View, файл HelpByCoursePage");
                return View(update, userState);
            }
        }

        public PageResultBase Handle(Update update, UserState userState)
        {
            try
            {
                if (update.Message != null)
                {
                    return View(update, userState);
                }
                if (update.CallbackQuery == null)
                    return new PageResultBase("Выберите действие с помощью кнопок", GetKeyboard());
                if (update.CallbackQuery.Data == "Назад")
                {
                    userState.Pages.Pop();
                    return userState.CurrenntPage.View(update, userState);
                }
                if (update.CallbackQuery.Data == "CommonQuestionsPage")
                {
                    return new CommonQuestionsPage().View(update, userState);
                }
                if (update.CallbackQuery.Data == "ResolveTaskPage")
                {
                    return new ResolveTaskPage().View(update, userState);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе Handle, файл HelpByCoursePage");
                return View(update, userState);
            }

            return View(update, userState);
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            try
            {
                var button1 = InlineKeyboardButton.WithCallbackData("Общий вопрос по изучаемой теме", "CommonQuestionsPage");
                var button2 = InlineKeyboardButton.WithCallbackData("Вопрос по конкретной задаче", "ResolveTaskPage");
                var button3 = InlineKeyboardButton.WithCallbackData("Назад", "Назад");

                return new InlineKeyboardMarkup(new[]
        {
        new[] { button1 },
        new[] { button2 },
        new[] {button3 }
        });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex} в методе GetKeyboard, файл HelpByCoursePage");
                return null;
            }
        }
    }
}