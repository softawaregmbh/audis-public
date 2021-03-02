namespace Audis.OpenID.Authentication.Configuration
{
    public class AuthenticationSettings
    {
        public string Issuer { get; set; } = default!;

        public string ClientId { get; set; } = default!;

        public string ClientSecret { get; set; } = default!;

        public string ScopeApiPath { get; set; } = default!;

        public void Deconstruct(out string clientId, out string clientSecret)
        {
            clientId = this.ClientId;
            clientSecret = this.ClientSecret;
        }

        public void Deconstruct(out string issuer, out string clientId, out string clientSecret)
        {
            issuer = this.Issuer;
            clientId = this.ClientId;
            clientSecret = this.ClientSecret;
        }
    }
}
