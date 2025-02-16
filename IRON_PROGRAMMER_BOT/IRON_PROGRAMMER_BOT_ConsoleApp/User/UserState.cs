using System.Collections.Generic;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.User
{
    public record class UserState(Stack<IPage> Pages, UserData UserData)
    {
        public IPage CurrenntPage => Pages.Peek();

        public void AddPage(IPage page)
        {
            if (CurrenntPage.GetType() != page.GetType())
            {
                Pages.Push(page);
            }
        }
    }
}
