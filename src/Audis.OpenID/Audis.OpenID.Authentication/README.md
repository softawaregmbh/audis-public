# Audis.OpenID.Authentication

The `Audis.OpenID.Authentication`-NuGet-Package makes sending authenticated requests to any protected AUDIS endpoint easier. The following goes into more detail regarding the different utilities offered by the package.

## Configuration of the NuGet package

In order to use the NuGet package, certain credentials have to be configured:

```json
{
    "Authentication": {
        "Issuer": "http://...",
        "ClientId": "...",
        "ClientSecret": "...",
        "ScopeApiPath": "http://..."
    }
}
```

`Issuer` specifies the identifying URL of the identity server. `ClientId` and `ClientSecret` are the credentials used for the M2M authentication flow in OpenId. `ScopeApiPath` is the URL to an endpoint that provides the available scopes for a specific AUDIS tenant.

In order to use the utilities provided by the NuGet package, they have to be added in the `StartUp` class of your .NET project. Simply call the extension method `static void AddClientAuthentication(this IServiceCollection services)`. It automatically registers a singleton instance of the `AuthenticationHandler` class (see below).

## AuthenticationHandler

The class `AuthenticationHandler` has multiple uses: 
1. extract an existing access token from the current request
2. request a new access token from the identity server
3. request the possible scopes for a specific AUDIS tenant
4. request the discovery document from the identity server

### Extract an existing access token

To extract an existing access token from the current HTTP request, the `GetOrCreateTokenAsync(string tenantId, CancellationToken cancellationToken = default)` method is used. It checks the headers of the current HTTP request and, if the `Authorization` header is set, returns the token. If said header is not set, instead a new access token for the tenant is requested from the identity server.

### Request a new token

If for some reason a new acess token is required, the method `RequestClientCredentialsTokenAsync(string tenantId, CancellationToken cancellationToken = default)` is used. In order to use this method, the fields `ClientId` and `ClientSecret` must have been configured, as they are used for authenticating against the identity server. If authentication was not successful, an `AuthenticationException` is thrown. Otherwise, the access token is returned.

### Get available scopes for tenant

To request the possible scopes of a tenant, the method `GetScopesForTenantAsync(string tenantId, CancellationToken cancellationToken = default)` is used. In order to use this method, the fields `ClientId` and `ScopeApiPath` must have been configured. If the request was successful, the available scopes are returned as a single string, separated by blanks.

### Requesting the discovery document

If for some reason the discovery document is needed, the method `GetDiscoveryDocumentAsync(CancellationToken cancellationToken = default)` can be used to request it. In order to use this method, the field `Issuer` must have been configured. If the request was successful, an instance of the class `OpenIddictDiscoveryDocument` is returned. Otherwise, an `AuthenticationException` is thrown.

## Utility methods

The NuGet package provides two extension methods for handling the `Authorization` HTTP header: `TryGetAuthorizationCodeToken(this HttpRequest httpRequest, out StringValues accessToken)` and `TrySetAuthorizationHeader(this HttpClient httpClient, StringValues accessToken)`, with the former allowing to extract an existing access token from the header of a request (without requesting a new access token if none is found), while the latter is used to set the respective HTTP header on a HTTP client.