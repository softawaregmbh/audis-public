# Audis.OpenID.Authorization

The `Audis.OpenID.Authorization`-NuGet-Package makes protecting endpoints against unauthorized requests easier. The following goes into more detail regarding the different utilities offered by the package.


## Configuration of the NuGet package

In order to use the NuGet package, certain credentials have to be configured:

```json
{
    "Authorization": {
        "Issuer": "http://...",
        "Audience": "..."
    }
}
```

`Issuer` specifies the identifying URL of the identity server. `Audience` specifies the unique identifier used by the identity server to identify the protected endpoint.

In order to use the utilities provided by the NuGet package, they have to be added in the `StartUp` class of your .NET project. Simply call the extension method `static void AddResourceAuthorization(this IServiceCollection services)`. It automatically configures authorization using the settings configured in `appsettings.json`. If different credentials need to be used, the overload `static void AddResourceAuthorization(this IServiceCollection services, AuthorizationSettings authorizationSettings)` can be used. Additionally, `static void UseResourceAuthorization(this IApplicationBuilder app)` needs to called. Every controller or endpoint that should be authorized needs to be annotated with the `[Authorize]` attribute.