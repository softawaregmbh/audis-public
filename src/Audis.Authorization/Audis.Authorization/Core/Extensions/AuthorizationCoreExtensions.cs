using Microsoft.Extensions.Configuration;

namespace Audis.Authorization
{
    public static class AuthorizationCoreExtensions
    {
        public static AuthorizationSettings GetAuthorizationSettings(this IConfiguration configuration)
        {
            var authorizationSettings = configuration.GetSection("Authorization").Get<AuthorizationSettings>();

            AssertAtLeastOneConfigurationPresent(authorizationSettings);
            AssertClientAuthorizationSettings(authorizationSettings.Client);
            AssertResourceAuthorizationSettings(authorizationSettings.Resource);

            return authorizationSettings;
        }

        private static void AssertAtLeastOneConfigurationPresent(AuthorizationSettings settings)
        {
            if (settings == null || (settings.Client == null && settings.Resource == null))
            {
                throw new AuthorizationSettingsException("Authorization settings are missing");
            }
        }

        private static void AssertClientAuthorizationSettings(ClientAuthorizationSettings settings)
        {
            if (settings != null)
            {
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
            }
        }

        private static void AssertResourceAuthorizationSettings(ResourceAuthorizationSettings settings)
        {
            if (settings != null)
            {
                if (settings.Issuer == null)
                {
                    ThrowPropertyMissingException(nameof(settings.Issuer));
                }

                if (settings.ClientId == null)
                {
                    ThrowPropertyMissingException(nameof(settings.ClientId));
                }

                if (settings.Audience == null)
                {
                    ThrowPropertyMissingException(nameof(settings.Audience));
                }
            }
        }

        private static void ThrowConfigurationMissingException(string configurationKey) =>
            throw new AuthorizationSettingsException($"{configurationKey} authorization settings are missing");

        private static void ThrowPropertyMissingException(string property) =>
            throw new AuthorizationSettingsException($"Failed to setup authorization: Property '{property}' is not configured");

        public static ClientAuthorizationSettings GetClientAuthorizationSettings(this IConfiguration configuration)
        {
            var clientAuthorizationSettings = configuration.GetAuthorizationSettings().Client;
            if (clientAuthorizationSettings == null)
            {
                ThrowConfigurationMissingException("Client");
            }

            return clientAuthorizationSettings;
        }

        public static ResourceAuthorizationSettings GetResourceAuthorizationSettings(this IConfiguration configuration)
        {
            var resourceAuthorizationSettings = configuration.GetAuthorizationSettings().Resource;
            if (resourceAuthorizationSettings == null)
            {
                ThrowConfigurationMissingException("Resource");
            }

            return resourceAuthorizationSettings;
        }
    }
}
