using Audis.OpenID.Authorization.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Audis.Authorization.OpenID.Resource
{
    public static class AuthorizationExtensions
    {
        public static void AddResourceAuthorization(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var authorizationSettings = configuration.GetAuthorizationSettings();

            services.AddResourceAuthorization(authorizationSettings);
        }

        public static void AddResourceAuthorization(this IServiceCollection services, AuthorizationSettings authorizationSettings)
        {
            var (issuer, audience) = authorizationSettings;

            services
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = issuer;
                    options.Audience = audience;
                });
        }

        public static void UseResourceAuthorization(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
