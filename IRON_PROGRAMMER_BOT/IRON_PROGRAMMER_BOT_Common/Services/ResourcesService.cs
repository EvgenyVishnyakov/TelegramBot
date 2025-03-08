using Serilog;
using Telegram.Bot.Types;

namespace IRON_PROGRAMMER_BOT_Common.Services
{
    public class ResourcesService
    {
        public InputFileStream? GetResource(byte[] buffer, string filename = "filename")
        {
            try
            {
                var memmoryStream = new MemoryStream(buffer);
                return InputFile.FromStream(memmoryStream, filename);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex}");
                return null;
            }
        }
    }
}
