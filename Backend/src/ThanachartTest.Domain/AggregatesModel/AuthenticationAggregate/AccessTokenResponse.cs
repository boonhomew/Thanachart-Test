using System.Text.Json.Serialization;

namespace ThanachartTest.Domain.AggregatesModel.AuthenticationAggregate
{
    public class AccessTokenResponse
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        public TokenSession ToCreateAccountSession()
        {
            return new TokenSession
            {
                TokenType = this.TokenType,
                AccessToken = this.AccessToken,
                RefreshToken = this.RefreshToken,
                ExpiresOn = this.ExpiresIn
            };
        }
    }
}
