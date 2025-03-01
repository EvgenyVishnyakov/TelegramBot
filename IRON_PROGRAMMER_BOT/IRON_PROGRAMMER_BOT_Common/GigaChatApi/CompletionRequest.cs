namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class CompletionRequest
    {
        public string AccessToken { get; private set; }
        //public string ContentType { get; set; } = "application/json";
        public GigaChatCompletionRequest RequestData { get; set; }

        public CompletionRequest(string AccessToken, string Prompt, CompletionSettings settings) : this(AccessToken, new List<GigaChatMessage>(), settings)
        {
            RequestData.MessageCollection = new List<GigaChatMessage>()
            {
                new GigaChatMessage()
                {
                    Content = Prompt,
                    Role = CompletionRolesEnum.system.ToString()
                }
            };
        }

        public CompletionRequest(string accessToken, IEnumerable<GigaChatMessage> messageHistory, CompletionSettings settings)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(AccessToken));
            }

            AccessToken = accessToken;

            RequestData = new GigaChatCompletionRequest() { MessageCollection = messageHistory };

            if (settings != null)
            {
                RequestData.Model = settings.Model;
                RequestData.Temperature = settings.Temperature;
                RequestData.TopP = settings.TopP;
                RequestData.Count = settings.Count;
            }
        }

        //public CompletionRequest(string accessToken, GigaChatCompletionRequest requestData)
        //{
        //    if (string.IsNullOrEmpty(AccessToken))
        //    {
        //        throw new ArgumentNullException(nameof(AccessToken));
        //    }

        //    AccessToken = accessToken;
        //    RequestData = requestData;
        //}
    }
}