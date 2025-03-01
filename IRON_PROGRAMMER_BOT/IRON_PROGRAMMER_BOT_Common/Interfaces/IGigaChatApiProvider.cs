using IRON_PROGRAMMER_BOT_Common.GigaChatApi;

namespace IRON_PROGRAMMER_BOT_Common.Interfaces
{
    public interface IGigaChatApiProvider
    {
        Task<AuthorizationResponse> AuthenticateAsync();
        Task<AuthorizationResponse> EnsureAuthenticatedAsync(Guid? RqUId = null, TimeSpan? reserveTime = null);
    }
}