using IRON_PROGRAMMER_BOT_Common.Interfaces;

namespace IRON_PROGRAMMER_BOT_Common.User
{
    public record class UserState(Stack<IPage> Pages, UserData UserData)
    {
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
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
