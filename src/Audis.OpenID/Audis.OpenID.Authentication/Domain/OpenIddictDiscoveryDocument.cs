using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Audis.OpenID.Authentication.Domain
{
    public class OpenIddictDiscoveryDocument
    {
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        [JsonPropertyName("authorization_endpoint")]
        public string AuthorizationEndpoint { get; set; }

        [JsonPropertyName("token_endpoint")]
        public string TokenEndpoint { get; set; }

        [JsonPropertyName("scopes_supported")]
        public IEnumerable<string> ScopesSupported { get; set; }
    }
}
