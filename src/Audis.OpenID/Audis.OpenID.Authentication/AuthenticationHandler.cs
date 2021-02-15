using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Audis.OpenID.Authentication.Domain;
using Audis.OpenID.Authentication.Exceptions;
using Audis.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using OpenIddict.Abstractions;

namespace Audis.OpenID.Authentication
{
    public class AuthenticationHandler
    {
        private readonly AuthenticationSettings authorizationSettings;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthenticationHandler(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            this.authorizationSettings = configuration.GetAuthenticationSettings();
            this.httpClientFactory = httpClientFactory ?? throw new System.ArgumentNullException(nameof(httpClientFactory));
            this.httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<StringValues> GetOrCreateTokenAsync(TenantId tenantId, CancellationToken cancellationToken = default)
        {
            if (!this.httpContextAccessor.HttpContext.Request.TryGetAuthorizationCodeToken(out var accessToken))
            {
                accessToken = await this.RequestClientCredentialsTokenAsync(tenantId, cancellationToken);
            }

            return accessToken;
        }

        public async Task<StringValues> RequestClientCredentialsTokenAsync(TenantId tenantId, CancellationToken cancellationToken = default)
        {
            var (clientId, clientSecret) = this.authorizationSettings;


            var discoveryDocument = await this.GetDiscoveryDocumentAsync(cancellationToken);
            var scope = await this.GetScopesForTenantAsync(tenantId, cancellationToken);

            var tokenRequest = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["scope"] = scope
            });

            using var httpClient = this.httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync(discoveryDocument.TokenEndpoint, tokenRequest, cancellationToken);
            var content = await response.Content.ReadFromJsonAsync<OpenIddictResponse>(cancellationToken: cancellationToken);

            return content.AccessToken ?? StringValues.Empty;
        }

        public async Task<string> GetScopesForTenantAsync(TenantId tenantId, CancellationToken cancellationToken = default)
        {
            var scopeApiUrl = this.authorizationSettings.ScopeApiPath
                .Replace(" ", string.Empty)
                .Replace("{{tenantId}}", tenantId.Value);
            
            using var httpClient = this.httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(scopeApiUrl, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new AuthenticationException($"Could not fetch scope for tenant \"{tenantId}\" at URL: {scopeApiUrl}");
            }

            var scopes = await response.Content.ReadFromJsonAsync<string[]>(cancellationToken: cancellationToken);

            return string.Join(" ", scopes);
        }

        public async Task<OpenIddictDiscoveryDocument> GetDiscoveryDocumentAsync(CancellationToken cancellationToken = default)
        {
            var issuer = this.authorizationSettings.Issuer.TrimEnd('/');
            var discoveryDocumentRequestUrl = $"{issuer}/.well-known/openid-configuration";

            using var httpClient = this.httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(discoveryDocumentRequestUrl, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new AuthenticationException($"Could not fetch discovery document at URL: {discoveryDocumentRequestUrl}");
            }

            var discoveryDocument = await response.Content.ReadFromJsonAsync<OpenIddictDiscoveryDocument>(cancellationToken: cancellationToken);

            return discoveryDocument;
        }
    }
}
