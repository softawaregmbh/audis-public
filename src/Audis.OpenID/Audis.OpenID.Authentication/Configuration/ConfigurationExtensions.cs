using Audis.OpenID.Authorization.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Audis.OpenID.Authentication
{
    public static class ConfigurationExtensions
    {
        public static AuthenticationSettings GetAuthenticationSettings(this IConfiguration configuration)
        {
            var authenticationSettings = configuration.GetSection("Authentication").Get<AuthenticationSettings>();

            AssertClientAuthorizationSettings(authenticationSettings);

            return authenticationSettings;
        }

        private static void AssertClientAuthorizationSettings(AuthenticationSettings settings)
        {
            if (settings == null)
            {
                ThrowConfigurationMissingException();
            }

            if (settings.Issuer == null)
            {
                ThrowPropertyMissingException(nameof(settings.Issuer));
            }

            if (settings.ClientId == null)
            {
                ThrowPropertyMissingException(nameof(settings.ClientId));
            }

            if (settings.ClientSecret == null)
            {
                ThrowPropertyMissingException(nameof(settings.ClientSecret));
            }

            if (settings.ScopeApiPath == null)
            {
                ThrowPropertyMissingException(nameof(settings.ScopeApiPath));
            }
        }

        private static void ThrowConfigurationMissingException() =>
            throw new AuthenticationSettingsException($"Failed to setup authentication: Configuration is missing");

        private static void ThrowPropertyMissingException(string property) =>
            throw new AuthenticationSettingsException($"Failed to setup authentication: Property '{property}' is not configured");
    }
}
