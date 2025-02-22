using System;
using System.IO;
using System.Linq;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.Services
{
    public class ResourcesService
    {
        public static InputFileStream GetResource(string path)
        {
            try
            {
                var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                var filename = path.Split("//").Last();
                return InputFile.FromStream(fileStream, filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
