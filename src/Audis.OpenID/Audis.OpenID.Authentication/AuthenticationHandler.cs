using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Audis.OpenID.Authentication.Configuration;
using Audis.OpenID.Authentication.Domain;
using Audis.OpenID.Authentication.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using OpenIddict.Abstractions;

namespace Audis.OpenID.Authentication
{
    public class AuthenticationHandler
    {
        private readonly AuthenticationSettings authenticationSettings;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthenticationHandler(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            this.authenticationSettings = configuration.GetAuthenticationSettings();
            this.httpClientFactory = httpClientFactory ?? throw new System.ArgumentNullException(nameof(httpClientFactory));
            this.httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<StringValues> GetOrCreateTokenAsync(string tenantId, CancellationToken cancellationToken = default)
        {
            if (!this.httpContextAccessor.HttpContext.Request.TryGetAuthorizationCodeToken(out var accessToken))
            {
                accessToken = await this.RequestClientCredentialsTokenAsync(tenantId, cancellationToken);
            }

            return accessToken;
        }

        public async Task<StringValues> RequestClientCredentialsTokenAsync(string tenantId, CancellationToken cancellationToken = default)
        {
            var (clientId, clientSecret) = this.authenticationSettings;
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

            if (!response.IsSuccessStatusCode)
            {
                throw new AuthenticationException($"Could not fetch access token from endpoint {discoveryDocument.TokenEndpoint}: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadFromJsonAsync<OpenIddictResponse>(cancellationToken: cancellationToken);

            return content?.AccessToken ?? StringValues.Empty;
        }

        public async Task<string> GetScopesForTenantAsync(string tenantId, CancellationToken cancellationToken = default)
        {
            var requestUrl = this.authenticationSettings.ScopeApiPath
                .Replace(" ", string.Empty)
                .Replace("{{tenantId}}", HttpUtility.UrlEncode(tenantId))
                .Replace("{{clientId}}", HttpUtility.UrlEncode(this.authenticationSettings.ClientId));
            
            using var httpClient = this.httpClientFactory.CreateClient();
            
            var response = await httpClient.GetAsync(requestUrl, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new AuthenticationException($"Could not fetch scope for tenant \"{tenantId}\" at URL: {requestUrl}");
            }

            var scopes = await response.Content.ReadFromJsonAsync<string[]>(cancellationToken: cancellationToken);
            if (scopes == null)
            {
                return string.Empty;
            }

            return string.Join(" ", scopes);
        }

        public async Task<OpenIddictDiscoveryDocument> GetDiscoveryDocumentAsync(CancellationToken cancellationToken = default)
        {
            var issuer = this.authenticationSettings.Issuer.TrimEnd('/');
            var discoveryDocumentRequestUrl = $"{issuer}/.well-known/openid-configuration";

            using var httpClient = this.httpClientFactory.CreateClient();
            
            var response = await httpClient.GetAsync(discoveryDocumentRequestUrl, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new AuthenticationException($"Could not fetch discovery document at URL: {discoveryDocumentRequestUrl}");
            }

            var discoveryDocument = await response.Content.ReadFromJsonAsync<OpenIddictDiscoveryDocument>(cancellationToken: cancellationToken);
            if (discoveryDocument == null)
            {
                throw new AuthenticationException($"Could not fetch discovery document at URL: {discoveryDocumentRequestUrl}");
            }

            return discoveryDocument;
        }
    }
}
