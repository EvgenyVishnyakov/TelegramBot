namespace IRON_PROGRAMMER_BOT_Common.Configuration
{
    public class FirebaseConfiguration
    {
        public const string SectionName = "Firebase";

        public string BasePath { get; set; } = Environment.GetEnvironmentVariable("BasePath")!;
        public string Secret { get; set; } = Environment.GetEnvironmentVariable("Secret")!;
    }
}
