using System.Text.Json.Serialization;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class GigaChatCompletionResponse
    {
        [JsonPropertyName("choices")]
        public IEnumerable<GigaChatChoice> Choices { get; set; }

        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("usage")]
        public GigaChatUsage Usage { get; set; }


        [JsonPropertyName("object")]
        public string Obj { get; set; }
    }
}