
namespace IRON_PROGRAMMER_BOT_ConsoleApp.User
{
    public class UserData
    {
        public string? StepiId { get; set; }

        public string? UserQuastion { get; set; }
        public HelperBotMessage? LastMessage { get; set; }

        public override string ToString()
        {
            return $"StepiId = {StepiId}";
        }
    }
}
