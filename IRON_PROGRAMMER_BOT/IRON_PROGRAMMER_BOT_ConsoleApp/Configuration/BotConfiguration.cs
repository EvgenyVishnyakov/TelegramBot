using System;

namespace IRON_PROGRAMMER_BOT_ConsoleApp.Configuration
{
    public class BotConfiguration
    {
        public const string SectionName = "BotConfiguration";

        public const string UpdateRoute = "webhook/update";

        public string BotToken { get; set; } = Environment.GetEnvironmentVariable("BotToken")!;
        public string HostAddress { get; set; } = Environment.GetEnvironmentVariable("HostAddress")!;
        public string SecretToken { get; set; } = Environment.GetEnvironmentVariable("SecretToken")!;
    }
}
