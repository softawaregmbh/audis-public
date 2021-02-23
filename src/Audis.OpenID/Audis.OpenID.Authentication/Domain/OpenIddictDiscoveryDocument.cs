using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Audis.OpenID.Authentication.Domain
{
    public class OpenIddictDiscoveryDocument
    {
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; } = default!;

        [JsonPropertyName("authorization_endpoint")]
        public string AuthorizationEndpoint { get; set; } = default!;

        [JsonPropertyName("token_endpoint")]
        public string TokenEndpoint { get; set; } = default!;

        [JsonPropertyName("scopes_supported")]
        public IEnumerable<string> ScopesSupported { get; set; } = Enumerable.Empty<string>();
    }
}
