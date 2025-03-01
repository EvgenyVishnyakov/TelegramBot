using System.Text.Json;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class CompletionResponse
    {
        public GigaChatCompletionResponse? GigaChatCompletionResponse { get; set; }
        public bool RequestSuccessed { get; set; }
        public string? ErrorTextIfFailed { get; set; }

        public CompletionResponse(HttpResponseMessage HttpMsg)
        {
            string responseVal = HttpMsg.Content.ReadAsStringAsync().Result;

            if (HttpMsg.StatusCode == System.Net.HttpStatusCode.OK)
            {
                RequestSuccessed = true;
                GigaChatCompletionResponse = JsonSerializer.Deserialize<GigaChatCompletionResponse>(responseVal);
            }
            else
            {
                RequestSuccessed = false;
                if (string.IsNullOrEmpty(responseVal))
                {
                    ErrorTextIfFailed = "See HttpResponse to get more information";
                }
                else
                {
                    ErrorTextIfFailed = responseVal;
                }
            }
        }
    }
}