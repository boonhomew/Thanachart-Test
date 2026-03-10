namespace ThanachartTest.Domain.AggregatesModel.AuthenticationAggregate
{
    public class TokenVerificationResponse
    {
        public bool IsValid { get; set; }
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
        public string Message { get; set; }
    }
}
