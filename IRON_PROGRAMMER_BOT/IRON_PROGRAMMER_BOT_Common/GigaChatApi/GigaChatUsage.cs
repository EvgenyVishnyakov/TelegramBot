using System.Text.Json.Serialization;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class GigaChatUsage
    {
        [JsonPropertyName("prompt_tokens")]
        int PromptTokens { get; set; }

        [JsonPropertyName("completion_tokens")]
        int CompletionTokens { get; set; }

        [JsonPropertyName("total_tokens")]
        int TotalTokens { get; set; }
    }
}