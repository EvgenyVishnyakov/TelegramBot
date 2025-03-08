using System.Text.Json.Serialization;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class GigaChatCompletionRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = Resources.Model!;

        // private float? _temperature;

        [JsonPropertyName("temperature")]
        public float? Temperature { get; set; } = Convert.ToInt64(Resources.Temperature)!;

        //private float? _topP;

        [JsonPropertyName("top_p")]
        public float? TopP { get; set; } = Convert.ToInt64(Resources.TopP)!;

        //private long? _count;

        [JsonPropertyName("count")]
        public long? Count { get; set; } = Convert.ToInt64(Resources.Count)!;

        [JsonPropertyName("messages")]
        public IEnumerable<GigaChatMessage>? MessageCollection { get; set; }
    }
}