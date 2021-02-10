using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Audis.Authorization.Resource
{
    public static class AuthorizationResourceExtensions
    {

        public static void AddResourceAuthentication(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var authorizationSettings = configuration.GetAuthorizationSettings();

            services.AddResourceAuthentication(authorizationSettings);
        }

        public static void AddResourceAuthentication(this IServiceCollection services, AuthorizationSettings authorizationSettings)
        {
            var (issuer, audience) = authorizationSettings.Resource;

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

        public static void ConfigureResourceAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
