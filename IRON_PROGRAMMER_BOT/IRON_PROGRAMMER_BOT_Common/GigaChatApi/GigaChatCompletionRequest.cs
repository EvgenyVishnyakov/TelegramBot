using System.Text.Json.Serialization;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class GigaChatCompletionRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = Resources.Model!;

        [JsonPropertyName("temperature")]
        public float? Temperature { get; set; } = float.Parse(Resources.Temperature)!;

        [JsonPropertyName("top_p")]
        public float? TopP { get; set; } = float.Parse(Resources.TopP);

        [JsonPropertyName("count")]
        public long? Count { get; set; } = long.Parse(Resources.Count);

        [JsonPropertyName("messages")]
        public IEnumerable<GigaChatMessage>? MessageCollection { get; set; }
    }
}