using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Audis.OpenID.Authentication
{
    public static class AuthenticationExtensions
    {
        public static void AddClientAuthentication(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<AuthenticationHandler>();
        }

        public static bool TryGetAuthorizationCodeToken(this HttpRequest httpRequest, out StringValues accessToken) =>
            httpRequest.Headers.TryGetValue("Authorization", out accessToken);

        public static void TrySetAuthorizationHeader(this HttpClient httpClient, StringValues accessToken)
        {
            if (AuthenticationHeaderValue.TryParse(accessToken, out var authenticationHeaderValue))
            {
                httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
            }
        }
    }
}
