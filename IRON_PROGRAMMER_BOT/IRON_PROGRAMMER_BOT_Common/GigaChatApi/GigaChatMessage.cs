using System.Text.Json.Serialization;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class GigaChatMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = "system";

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}