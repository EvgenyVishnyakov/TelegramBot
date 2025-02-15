using System.IO;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.Services
{
    public class ResourcesService
    {
        public static InputFileStream GetResource(string path)
        {
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return InputFile.FromStream(fileStream);
        }

    }
}
