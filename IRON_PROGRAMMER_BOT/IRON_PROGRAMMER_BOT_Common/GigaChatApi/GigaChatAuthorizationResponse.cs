using System.Text.Json.Serialization;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class GigaChatAuthorizationResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_at")]
        public long? ExpiresAt { get; set; }

        private DateTime? _expiresAtDateTime = null;

        public DateTime? ExpiresAtDateTime
        {
            get
            {
                if (!ExpiresAt.HasValue)
                {
                    return null;
                }

                if (_expiresAtDateTime == null)
                {
                    TimeSpan ts = TimeSpan.FromMilliseconds(ExpiresAt.Value);
                    _expiresAtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc) + ts;
                    _expiresAtDateTime = _expiresAtDateTime.Value.ToLocalTime();
                }

                return _expiresAtDateTime.Value;
            }
        }
    }
}