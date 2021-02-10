namespace Audis.Authorization
{
    public class AuthorizationSettings
    {

        public ClientAuthorizationSettings Client { get; set; } = null;

        public ResourceAuthorizationSettings Resource { get; set; } = null;
    }
}
