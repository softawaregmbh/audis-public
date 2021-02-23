namespace Audis.OpenID.Authorization.Configuration
{
    public class AuthorizationSettings
    {
        public string Issuer { get; set; } = default!;

        public string Audience { get; set; } = default!;

        public void Deconstruct(out string issuer, out string audience)
        {
            issuer = this.Issuer;
            audience = this.Audience;
        }
    }
}
