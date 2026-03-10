namespace ThanachartTest.Domain.AggregatesModel.AuthenticationAggregate.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);

        Task<bool> RegisterAsync(RegisterRequest request);

        JWKsResponse ReadJWKs();

        TokenVerificationResponse VerifyToken(string token);

    }
}
