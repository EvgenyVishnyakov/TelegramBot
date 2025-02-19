using System;
using System.Linq;
using System.Reflection;
using IRON_PROGRAMMER_BOT_ConsoleApp.User.Pages;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.Firebase
{
    public static class PagesFactory
    {
        public static IPage GetPage(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetTypes().FirstOrDefault(t => t.Name == name && typeof(IPage).IsAssignableFrom(t));

            return (IPage)Activator.CreateInstance(type);
        }
    }
}
