using IRON_PROGRAMMER_BOT_Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

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
                Log.Error($"{ex.Message} в файле PagesFactory");
                return null;
            }
        }
    }
}
