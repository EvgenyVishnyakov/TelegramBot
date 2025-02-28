namespace IRON_PROGRAMMER_BOT_Common.GigaChatApi
{
    public class AuthorizationRequest(Guid? requestID = null)
    {
        public Guid RqUID { get; set; } = requestID ?? Guid.NewGuid();
        public string AuthorizationID { get; set; } = Environment.GetEnvironmentVariable("SecretKey")!;
        public RateScope RateScope { get; set; } = RateScope.GIGACHAT_API_PERS;
    }
}
