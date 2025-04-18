﻿using IRON_PROGRAMMER_BOT_Common.Interfaces;
using Serilog;

namespace IRON_PROGRAMMER_BOT_Common.User
{
    public record class UserState(Stack<IPage> Pages, UserData UserData)
    {
        public int requestCounter { get; set; }

        public bool IsPassword { get; set; }
        public IPage CurrentPage => Pages.Peek();

        public void AddPage(IPage page)
        {
            try
            {
                if (CurrentPage.GetType() != page.GetType())
                {
                    Pages.Push(page);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
    }
}
