using System.Text.Json;
using Serilog;

namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class Completion
    {
        public CompletionRequest? LastRequest { get; private set; }
        public CompletionResponse? LastResponse { get; private set; }
        public List<GigaChatMessage> History { get; set; } = new List<GigaChatMessage>();

        public async Task<CompletionResponse?> SendRequest(string token, string message, bool useHistory = false, CompletionSettings requestSettings = null)
        {
            try
            {
                CompletionRequest? request = null;

                if (useHistory)
                {
                    History.Add(new GigaChatMessage()
                    {
                        Content = message,
                        Role = CompletionRolesEnum.system.ToString()
                    });

                    request = new CompletionRequest(token, History, requestSettings);
                }
                else
                {
                    request = new CompletionRequest(token, message, requestSettings);
                }

                LastRequest = request;
                return await SendRequestToService(request, useHistory);
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка {ex.Message.ToString()} в методе SendRequest класса Completion");
                return null;
            }
        }

        private async Task<CompletionResponse?> SendRequestToService(CompletionRequest request, bool useHistory)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add(RequestConstants.AuthorizationHeaderTitle, $"Bearer {request.AccessToken}");

                var data = JsonSerializer.Serialize(request.RequestData, typeof(GigaChatCompletionRequest));
                var response = await client.PostAsync(EndPoints.CompletionURL, new StringContent(data));

                CompletionResponse result = new CompletionResponse(response);
                LastResponse = result;

                if (LastResponse != null && LastResponse.RequestSuccessed)
                {
                    if (useHistory)
                    {
                        foreach (var choice in LastResponse.GigaChatCompletionResponse!.Choices!)
                        {
                            var message = choice.Message;

                            if (message != null)
                                History.Add(message);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка {ex.Message.ToString()} в методе SendRequestToService класса Completion");
                return null;
            }
        }
    }
}
