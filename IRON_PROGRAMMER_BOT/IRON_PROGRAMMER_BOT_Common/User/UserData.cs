namespace IRON_PROGRAMMER_BOT_Common.User
{
    public class UserData()
    {
        public string? StepiId { get; set; }
        public long TelegramId { get; set; }

        public string? UserQuastion { get; set; }
        public HelperBotMessage? LastMessage { get; set; }
        public string? SelectedCourseId { get; set; }
        public string? NameCourse { get; set; }

        public override string ToString()
        {
            return $"StepiId = {StepiId}";
        }
    }
}
