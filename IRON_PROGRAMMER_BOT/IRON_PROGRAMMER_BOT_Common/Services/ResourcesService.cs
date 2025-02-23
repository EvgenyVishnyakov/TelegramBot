using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.Services
{
    public class ResourcesService
    {
        public static InputFileStream GetResource(string path)
        {
            try
            {
                var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                var filename = Path.GetFileName(path);
                return InputFile.FromStream(fileStream, filename);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }
    }
}
