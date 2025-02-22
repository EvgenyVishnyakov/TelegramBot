using System;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.Configuration
{
    public class FirebaseConfiguration
    {
        public const string SectionName = "Firebase";

        public string BasePath { get; set; } = Environment.GetEnvironmentVariable("HostAddress")!;
        public string Secret { get; set; } = Environment.GetEnvironmentVariable("HostAddress")!;
    }
}
