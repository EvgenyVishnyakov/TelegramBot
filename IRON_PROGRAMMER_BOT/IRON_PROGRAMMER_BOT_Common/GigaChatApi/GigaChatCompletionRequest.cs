using System.Text.Json.Serialization;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class GigaChatCompletionRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "GigaChat:latest";

        private float? _temperature;

        [JsonPropertyName("temperature")]
        public float? Temperature
        {
            get => _temperature;
            set => _temperature = value == null ? null : value < 0 ? 0 : value > 2 ? 2 : value;
        }

        private float? _topP;

        [JsonPropertyName("top_p")]
        public float? TopP
        {
            get => _topP;
            set => _topP = value == null ? null : value < 0 ? 0 : value > 1 ? 1 : value;
        }

        private long? _count;

        [JsonPropertyName("count")]
        public long? Count
        {
            get => _count;
            set => _count = value == null ? null : value < 1 ? 1 : value > 4 ? 4 : value;
        }

        [JsonPropertyName("messages")]
        public IEnumerable<GigaChatMessage>? MessageCollection { get; set; }
    }
}