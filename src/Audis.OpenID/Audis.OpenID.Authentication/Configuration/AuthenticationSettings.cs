namespace Audis.OpenID.Authentication
{
    public class AuthenticationSettings
    {
        public string Issuer { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string ScopeApiPath { get; set; }

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
