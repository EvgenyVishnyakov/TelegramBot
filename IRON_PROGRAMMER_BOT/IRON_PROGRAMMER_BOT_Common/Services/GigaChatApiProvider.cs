﻿using IRON_PROGRAMMER_BOT_Common.GigaChatApi;
using IRON_PROGRAMMER_BOT_Common.Interfaces;

namespace IRON_PROGRAMMER_BOT_Common.Services
{
    public class GigaChatApiProvider(HttpClient httpClient, AuthorizationRequest lastRequest) : IGigaChatApiProvider
    {
        private TimeSpan tokenExpiration;
        public AuthorizationRequest LastRequest { get; set; } = lastRequest;
        public AuthorizationResponse? LastResponse { get; set; }

        public async Task<AuthorizationResponse> AuthenticateAsync()
        {
            httpClient.DefaultRequestHeaders.Add(RequestConstants.AuthorizationHeaderTitle, $"Bearer {LastRequest.AuthorizationID}");
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

        private async Task<AuthorizationResponse> EnsureAuthenticatedAsync(Guid? RqUId = null, TimeSpan? reserveTime = null)
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

        private async Task EnsureAuthenticatedAsync(object value)
        {
            throw new NotImplementedException();
        }
    }
}
