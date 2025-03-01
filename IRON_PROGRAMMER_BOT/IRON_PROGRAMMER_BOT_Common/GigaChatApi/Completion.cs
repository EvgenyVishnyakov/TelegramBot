﻿using System.Text.Json;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class Completion
    {
        public CompletionRequest LastRequest { get; private set; }
        public CompletionResponse LastResponse { get; private set; }
        public List<GigaChatMessage> History { get; set; } = new List<GigaChatMessage>();

        public async Task<CompletionResponse> SendRequest(string Token, string Message, bool useHistory = true, CompletionSettings requestSettings = null)
        {
            CompletionRequest request = null;

            if (useHistory)
            {
                History.Add(new GigaChatMessage()
                {
                    Content = Message,
                    Role = CompletionRolesEnum.system.ToString()
                });

                request = new CompletionRequest(Token, History, requestSettings);
            }
            else
            {
                request = new CompletionRequest(Token, Message, requestSettings);
            }

            LastRequest = request;
            return await SendRequestToService(request, useHistory);
        }

        private async Task<CompletionResponse> SendRequestToService(CompletionRequest request, bool useHistory)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add(RequestConstants.AuthorizationHeaderTitle, $"Bearer {request.AccessToken}");

            string data = JsonSerializer.Serialize(request.RequestData, typeof(GigaChatCompletionRequest));
            var response = await client.PostAsync(EndPoints.CompletionURL, new StringContent(data));

            CompletionResponse result = new CompletionResponse(response);
            LastResponse = result;

            if (LastResponse != null && LastResponse.RequestSuccessed)
            {
                if (useHistory)
                {
                    foreach (var it in LastResponse.GigaChatCompletionResponse?.Choices)
                    {
                        var msg = it.Message;

                        if (msg != null)
                            History.Add(msg);
                    }
                }
            }

            return result;
        }
    }
}
