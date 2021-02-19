using Audis.OpenID.Authorization.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Audis.OpenID.Authorization.Configuration
{
    public static class AuthorizationCoreExtensions
    {
        public static AuthorizationSettings GetAuthorizationSettings(this IConfiguration configuration)
        {
            var authorizationSettings = configuration.GetSection("Authorization").Get<AuthorizationSettings>();

            AssertAuthorizationSettings(authorizationSettings);

            return authorizationSettings;
        }

        private static void AssertAuthorizationSettings(AuthorizationSettings settings)
        {
            if (settings == null)
            {
                ThrowConfigurationMissingException();
            }

            if (settings.Issuer == null)
            {
                ThrowPropertyMissingException(nameof(settings.Issuer));
            }

            if (settings.Audience == null)
            {
                ThrowPropertyMissingException(nameof(settings.Audience));
            }
        }

        private static void ThrowConfigurationMissingException() =>
            throw new AuthorizationSettingsException($"Failed to setup authorization: Configuration is missing");

        private static void ThrowPropertyMissingException(string property) =>
            throw new AuthorizationSettingsException($"Failed to setup authorization: Property '{property}' is not configured");
    }
}
