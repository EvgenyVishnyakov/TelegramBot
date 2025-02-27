using System.Net.Http.Headers;
using System.Text;
using IRON_PROGRAMMER_BOT_Common.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class GigaChatApiProvider
    {
        private readonly HttpClient _httpClient;
        private readonly GigaChatApiConfiguration _options;
        private string accessToken;
        private DateTime tokenExpiration;

        public GigaChatApiProvider(HttpClient httpClient, IOptions<GigaChatApiConfiguration> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task AuthenticateAsync()
        {
            var authUrl = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";
            var authData = new FormUrlEncodedContent(
                [
                new KeyValuePair<string, string>("RqUID",Guid.NewGuid().ToString()),
                new KeyValuePair<string, string>("Token",_options.ClientSecret),
                new KeyValuePair<string, string>("Scope","GIGACHAT_API_PERS")
                ]
                );
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, authUrl)
            {
                Content = authData
            };

            var content = new StringContent("GIGACHAT_API_PERS", Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await _httpClient.PostAsync(authUrl, authData);


            //requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            //var response = await _httpClient.SendAsync(requestMessage);

            var jsonResponce = await response.Content.ReadAsStringAsync();
            var authResponce = JsonConvert.DeserializeObject<AuthResponce>(jsonResponce);

            Console.WriteLine(authResponce);

            accessToken = authResponce.AccessToken;
            tokenExpiration = DateTime.UtcNow.AddSeconds(authResponce.ExpiresAt);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        private async Task EnsureAuthenticatedAsync()
        {
            if (DateTime.UtcNow >= tokenExpiration)
            {
                await AuthenticateAsync();
            }
        }

        public async Task<string> GetAnswer(string text)
        {
            try
            {
                await EnsureAuthenticatedAsync();
                //var answer = await _httpClient.GetStringAsync();
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return string.Empty;
            }
        }
    }
}
