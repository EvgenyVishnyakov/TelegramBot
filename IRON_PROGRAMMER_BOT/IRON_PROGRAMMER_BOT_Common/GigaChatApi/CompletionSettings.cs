namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class CompletionSettings
    {
        public string Model { get; set; } = Resources.Model!;

        public float? Temperature { get; set; } = float.Parse(Resources.Temperature)!;

        public float? TopP { get; set; } = float.Parse(Resources.TopP);

        public long? Count { get; set; } = long.Parse(Resources.Count);

        public long? MaxTokens { get; set; } = long.Parse(Resources.MaxTokens)!;
    }
}