namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class CompletionSettings
    {
        public string Model { get; set; }

        private float? _temperature;

        public float? Temperature
        {
            get => _temperature;
            set => _temperature = value == null ? null : value < 0 ? 0 : value > 2 ? 2 : value;
        }

        private float? _topP;

        public float? TopP
        {
            get => _topP;
            set => _topP = value == null ? null : value < 0 ? 0 : value > 1 ? 1 : value;
        }

        private long? _count;

        public long? Count
        {
            get => _count;
            set => _count = value == null ? null : value < 1 ? 1 : value > 4 ? 4 : value;
        }

        private long? _maxTokens;

        public long? MaxTokens
        {
            get => _maxTokens;
            set => _maxTokens = value == null ? null : value < 1 ? 1 : value;
        }

        public CompletionSettings(string modelName, float? temperature, float? topP, long? maxTokens)
        {
            Model = modelName;
            Temperature = temperature;
            TopP = topP;
            MaxTokens = maxTokens;
        }
    }
}