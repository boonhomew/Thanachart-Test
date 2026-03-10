namespace ThanachartTest.Domain.AggregatesModel.AuthenticationAggregate
{
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
