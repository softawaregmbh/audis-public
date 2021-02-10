using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Audis.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using OpenIddict.Abstractions;

namespace Audis.Authorization.Client
{
    public class AuthorizationHandler
    {
        private readonly ResourceAuthorizationSettings authorizationSettings;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpContext httpContext;

        public AuthorizationHandler(IConfiguration configuration, IHttpClientFactory httpClientFactory, HttpContext httpContext)
        {
            this.authorizationSettings = configuration.GetResourceAuthorizationSettings();
            this.httpClientFactory = httpClientFactory ?? throw new System.ArgumentNullException(nameof(httpClientFactory));
            this.httpContext = httpContext ?? throw new System.ArgumentNullException(nameof(httpContext));
        }

        public async Task<StringValues> GetOrCreateTokenAsync(TenantId tenantId, CancellationToken cancellationToken = default)
        {
            var request = this.httpContext.Request;

            if (!GetAuthorizationCodeToken(request, out var accessToken))
            {
                accessToken = await this.RequestClientCredentialsToken(tenantId, cancellationToken);
            }

            return accessToken;
        }

        private static bool GetAuthorizationCodeToken(HttpRequest request, out StringValues accessToken) =>
           request.Headers.TryGetValue("Authorization", out accessToken);

        public async Task<StringValues> RequestClientCredentialsToken(TenantId tenantId, CancellationToken cancellationToken = default)
        {
            var (issuer, clientId, clientSecret) = this.authorizationSettings;

            var scope = await this.GetScopesForTenant(tenantId, cancellationToken);

            var tokenRequest = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["scope"] = scope
            });

            using var httpClient = this.httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync(issuer, tokenRequest, cancellationToken);
            var payload = await response.Content.ReadFromJsonAsync<OpenIddictResponse>(cancellationToken: cancellationToken);

            return payload.AccessToken ?? StringValues.Empty;
        }

        private async Task<string> GetScopesForTenant(TenantId tenantId, CancellationToken cancellationToken = default)
        {
            using var httpClient = this.httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync($"{this.authorizationSettings.Issuer}/{this.authorizationSettings.ScopeApiPath}/{tenantId}", cancellationToken);
            var scopes = await response.Content.ReadAsStringAsync(cancellationToken);

            return scopes;
        }
    }
}
