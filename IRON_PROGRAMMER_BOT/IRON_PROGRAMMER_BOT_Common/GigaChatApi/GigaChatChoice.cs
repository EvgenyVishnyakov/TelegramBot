using System.Text.Json.Serialization;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class GigaChatChoice
    {
        [JsonPropertyName("message")]
        public GigaChatMessage? Message { get; set; }
    }
}