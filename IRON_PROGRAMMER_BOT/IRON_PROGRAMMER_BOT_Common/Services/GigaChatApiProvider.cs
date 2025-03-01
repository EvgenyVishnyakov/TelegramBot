using IRON_PROGRAMMER_BOT_Common.GigaChatApi;
using IRON_PROGRAMMER_BOT_Common.Interfaces;

namespace IRON_PROGRAMMER_BOT_Common.Services
{
    public class GigaChatApiProvider(HttpClient httpClient, AuthorizationRequest lastRequest) : IGigaChatApiProvider
    {
        public AuthorizationRequest LastRequest { get; set; } = lastRequest;
        public AuthorizationResponse? LastResponse { get; set; }

        public async Task<AuthorizationResponse> AuthenticateAsync()
        {
            httpClient.DefaultRequestHeaders.Add(name: RequestConstants.AuthorizationHeaderTitle, value: $"Bearer {LastRequest.AuthorizationID}");
            httpClient.DefaultRequestHeaders.Add(RequestConstants.RequestIDHeaderTitle, LastRequest.RqUID.ToString());

            var data = new[]
            {
                    new KeyValuePair<string, string>(RequestConstants.RateScope, LastRequest.RateScope.ToString())
                };

            var httpResponse = await httpClient.PostAsync(EndPoints.AuthorizationURL, new FormUrlEncodedContent(data));
            var response = new AuthorizationResponse(httpResponse);
            LastResponse = response;
            var accessToken = response.GigaChatAuthorizationResponse!.AccessToken;
            return response;
        }

        public async Task<AuthorizationResponse> EnsureAuthenticatedAsync(Guid? RqUId = null, TimeSpan? reserveTime = null)
        {
            TimeSpan expiredTimeSpan = reserveTime ?? TimeSpan.Zero;
            if (LastResponse == null || LastResponse.GigaChatAuthorizationResponse?.ExpiresAtDateTime - expiredTimeSpan < DateTime.Now)
            {
                Guid rqUID = RqUId ?? Guid.NewGuid();
                LastRequest = new AuthorizationRequest(rqUID);
                LastResponse = await AuthenticateAsync();
                return LastResponse;
            }

            return LastResponse;
        }
    }
}
