using System.Text.Json.Serialization;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class GigaChatCompletionResponse
    {
        [JsonPropertyName("choices")]
        public IEnumerable<GigaChatChoice>? Choices { get; set; }
    }
}