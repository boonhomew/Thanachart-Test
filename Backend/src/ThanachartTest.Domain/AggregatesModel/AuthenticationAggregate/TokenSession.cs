
namespace ThanachartTest.Domain.AggregatesModel.AuthenticationAggregate
{
    public class TokenSession
    {
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long ExpiresOn { get; set; }
    }
}
