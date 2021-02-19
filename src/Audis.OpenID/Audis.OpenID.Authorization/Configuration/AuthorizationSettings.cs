namespace Audis.OpenID.Authorization.Configuration
{
    public class AuthorizationSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public void Deconstruct(out string issuer, out string audience)
        {
            issuer = this.Issuer;
            audience = this.Audience;
        }
    }
}
