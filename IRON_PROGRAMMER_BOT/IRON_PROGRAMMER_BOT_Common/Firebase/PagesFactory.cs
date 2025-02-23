using IRON_PROGRAMMER_BOT_Common.User.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace IRON_PROGRAMMER_BOT_Common.Firebase
{
    public class PagesFactory(IServiceProvider services)
    {
        public IPage GetPage(string typeName)
        {
            try
            {
                var type = Type.GetType(typeName) ?? throw new Exception("Такого типа нет в программе");
                return (IPage)services.GetRequiredService(type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
