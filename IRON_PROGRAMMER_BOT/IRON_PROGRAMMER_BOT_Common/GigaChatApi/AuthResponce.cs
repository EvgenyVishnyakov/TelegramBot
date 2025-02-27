using Newtonsoft.Json;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class AuthResponce
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_at")]
        public int ExpiresAt { get; set; }
    }
}