
using System.Text.Json;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class AuthorizationResponse
    {

        public HttpResponseMessage HttpResponse { get; set; }

        public GigaChatAuthorizationResponse? GigaChatAuthorizationResponse { get; set; }

        public bool AuthorizationSuccess { get; set; }

        public string ErrorTextIfFailed { get; set; }

        public AuthorizationResponse(HttpResponseMessage httpResponse)
        {
            HttpResponse = httpResponse;
            string responseVal = httpResponse.Content.ReadAsStringAsync().Result;

            if (HttpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                AuthorizationSuccess = true;
                GigaChatAuthorizationResponse = JsonSerializer.Deserialize<GigaChatAuthorizationResponse>(responseVal);
            }
            else
            {
                AuthorizationSuccess = false;
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