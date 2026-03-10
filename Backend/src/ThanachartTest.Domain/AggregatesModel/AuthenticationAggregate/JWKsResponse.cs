
namespace ThanachartTest.Domain.AggregatesModel.AuthenticationAggregate
{
    public class JWKsResponse
    {
        public IEnumerable<JWKResponse> Keys { get; private set; }

        public JWKsResponse(JWKResponse key)
        {
            Keys = new List<JWKResponse> { key };
        }

        public JWKsResponse(IEnumerable<JWKResponse> keys)
        {
            Keys = keys;
        }
    }

    public class JWKResponse
    {
        public string Kid { get; set; }
        public string Use { get; set; }
        public string Kty { get; set; }
        public string Alg { get; set; }
        public string E { get; set; }
        public string N { get; set; }
    }
}
