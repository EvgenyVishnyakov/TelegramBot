using IRON_PROGRAMMER_BOT_Common.User.Pages;

namespace IRON_PROGRAMMER_BOT_Common.User
{
    public record class UserState(Stack<IPage> Pages, UserData UserData)
    {
        public IPage CurrenntPage => Pages.Peek();

        public void AddPage(IPage page)
        {
            try
            {
                if (CurrenntPage.GetType() != page.GetType())
                {
                    Pages.Push(page);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
