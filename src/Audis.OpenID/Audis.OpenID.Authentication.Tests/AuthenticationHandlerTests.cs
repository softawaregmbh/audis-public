using Audis.OpenID.Authentication.Exceptions;
using Audis.Primitives;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Audis.OpenID.Authentication.Tests
{
    [TestClass]
    public class AuthenticationHandlerTests
    {
        [TestMethod]
        public async Task GetDiscoveryDocumentAsync()
        {
            var issuer = "http://test.openiddict.com";
            var mockConfiguration = GetMockConfiguration(issuer: issuer);
            var mockHttpClientFactory = GetMockHttpClientFactory((mockHttp) =>
            {
                mockHttp.Expect($"{issuer}/.well-known/openid-configuration")
                    .Respond("application/json", @"
                    {
                        ""issuer"": """ + issuer + @""",
                        ""authorization_endpoint"": ""TestAuthorizationEndpoint"",                        
                        ""token_endpoint"": ""TestTokenEndpoint"",
                        ""scopes_supported"": [ ""scope1"", ""scope2"" ]
                    }");
            });

            var mockHttpContext = A.Fake<HttpContext>();
            StringValues ignored = StringValues.Empty;
            A.CallTo(() => mockHttpContext.Request.Headers.TryGetValue("Authorization", out ignored))
                .Returns(false);

            var mockHttpContextAccessor = A.Fake<IHttpContextAccessor>();
            A.CallTo(() => mockHttpContextAccessor.HttpContext)
                .Returns(mockHttpContext);

            var authorizationHandler = new AuthenticationHandler(mockConfiguration, mockHttpClientFactory, mockHttpContextAccessor);
            var discoveryDocument = await authorizationHandler.GetDiscoveryDocumentAsync();

            Assert.AreEqual(issuer, discoveryDocument.Issuer);
            Assert.AreEqual("TestAuthorizationEndpoint", discoveryDocument.AuthorizationEndpoint);
            Assert.AreEqual("TestTokenEndpoint", discoveryDocument.TokenEndpoint);
            CollectionAssert.AreEqual(new[] { "scope1", "scope2" }, discoveryDocument.ScopesSupported.ToList());
        }

        [TestMethod]
        public async Task GetDiscoveryDocumentAsync_ResponseCode404_Throws()
        {
            var tenantId = TenantId.From("TestTenant");
            var issuer = "http://test.openiddict.com";
            var scopeApiPath = "http://somewhere.else.com/scope/{{tenantId}}";

            var mockConfiguration = GetMockConfiguration(issuer: issuer, scopeApiPath: scopeApiPath);
            var mockHttpClientFactory = GetMockHttpClientFactory((mockHttp) =>
            {
                mockHttp.Expect($"{issuer}/.well-known/openid-configuration")
                    .Respond(System.Net.HttpStatusCode.NotFound, "application/json", string.Empty);
            });

            var mockHttpContext = A.Fake<HttpContext>();
            StringValues ignored = StringValues.Empty;
            A.CallTo(() => mockHttpContext.Request.Headers.TryGetValue("Authorization", out ignored))
                .Returns(false);

            var mockHttpContextAccessor = A.Fake<IHttpContextAccessor>();
            A.CallTo(() => mockHttpContextAccessor.HttpContext)
                .Returns(mockHttpContext);

            var authorizationHandler = new AuthenticationHandler(mockConfiguration, mockHttpClientFactory, mockHttpContextAccessor);

            await Assert.ThrowsExceptionAsync<AuthenticationException>(() => authorizationHandler.GetDiscoveryDocumentAsync(), "Could not fetch discovery document at URL: http://test.openiddict.com/.well-known/openid-configuration");
        }

        [TestMethod]
        public async Task GetScopesForTenantAsync()
        {
            var tenantId = TenantId.From("TestTenant");
            var issuer = "http://test.openiddict.com";
            var scopeApiPath = "http://somewhere.else.com/scope/{{tenantId}}";

            var mockConfiguration = GetMockConfiguration(issuer: issuer, scopeApiPath: scopeApiPath);
            var mockHttpClientFactory = GetMockHttpClientFactory((mockHttp) =>
            {
                mockHttp.Expect($"http://somewhere.else.com/scope/{tenantId}")
                    .Respond("application/json", @"[ ""scope1"", ""scope2"" ]");
            });

            var mockHttpContext = A.Fake<HttpContext>();
            StringValues ignored = StringValues.Empty;
            A.CallTo(() => mockHttpContext.Request.Headers.TryGetValue("Authorization", out ignored))
                .Returns(false);

            var mockHttpContextAccessor = A.Fake<IHttpContextAccessor>();
            A.CallTo(() => mockHttpContextAccessor.HttpContext)
                .Returns(mockHttpContext);

            var authorizationHandler = new AuthenticationHandler(mockConfiguration, mockHttpClientFactory, mockHttpContextAccessor);
            var scope = await authorizationHandler.GetScopesForTenantAsync(tenantId);

            Assert.AreEqual("scope1 scope2", scope);
        }

        [TestMethod]
        public async Task GetScopesForTenantAsync_ResponseCode404_Throws()
        {
            var tenantId = TenantId.From("TestTenant");
            var issuer = "http://test.openiddict.com";
            var scopeApiPath = "http://somewhere.else.com/scope/{{tenantId}}";

            var mockConfiguration = GetMockConfiguration(issuer: issuer, scopeApiPath: scopeApiPath);
            var mockHttpClientFactory = GetMockHttpClientFactory((mockHttp) =>
            {
                mockHttp.Expect($"http://somewhere.else.com/scope/{tenantId}")
                    .Respond(System.Net.HttpStatusCode.NotFound, "application/json", string.Empty);
            });

            var mockHttpContext = A.Fake<HttpContext>();
            StringValues ignored = StringValues.Empty;
            A.CallTo(() => mockHttpContext.Request.Headers.TryGetValue("Authorization", out ignored))
                .Returns(false);

            var mockHttpContextAccessor = A.Fake<IHttpContextAccessor>();
            A.CallTo(() => mockHttpContextAccessor.HttpContext)
                .Returns(mockHttpContext);

            var authorizationHandler = new AuthenticationHandler(mockConfiguration, mockHttpClientFactory, mockHttpContextAccessor);

            await Assert.ThrowsExceptionAsync<AuthenticationException>(() => authorizationHandler.GetScopesForTenantAsync(tenantId), "Could not fetch scope for tenant \"TestTenant\" at URL: http://somewhere.else.com/scope/TestTenant");
        }

        [TestMethod]
        public async Task RequestClientCredentialsTokenAsync()
        {
            var tenantId = TenantId.From("TestTenant");
            var issuer = "http://test.openiddict.com";
            var scopeApiPath = "http://somewhere.else.com/scope/{{tenantId}}";

            var mockConfiguration = GetMockConfiguration(issuer: issuer, scopeApiPath: scopeApiPath);
            var mockHttpClientFactory = GetMockHttpClientFactory((mockHttp) =>
            {
                mockHttp.Expect($"{issuer}/.well-known/openid-configuration")
                    .Respond("application/json", @"{ ""token_endpoint"": """ + $"{issuer}/connect/token" + @""" }");

                mockHttp.Expect($"http://somewhere.else.com/scope/{tenantId}")
                    .Respond("application/json", @"[ ""audis.analyzer.prod"" ]");

                mockHttp.Expect($"{issuer}/connect/token")
                    .Respond("application/json", @"{ ""access_token"": ""Bearer TestAccessToken"" }");
            });

            var mockHttpContext = A.Fake<HttpContext>();
            StringValues ignored = StringValues.Empty;
            A.CallTo(() => mockHttpContext.Request.Headers.TryGetValue("Authorization", out ignored))
                .Returns(false);

            var mockHttpContextAccessor = A.Fake<IHttpContextAccessor>();
            A.CallTo(() => mockHttpContextAccessor.HttpContext)
                .Returns(mockHttpContext);

            var authorizationHandler = new AuthenticationHandler(mockConfiguration, mockHttpClientFactory, mockHttpContextAccessor);

            var token = await authorizationHandler.RequestClientCredentialsTokenAsync(tenantId);

            Assert.AreEqual(new StringValues("Bearer TestAccessToken"), token);
        }

        [TestMethod]
        public async Task GetOrCreateTokenAsync_AccessTokenPresent()
        {
            var mockConfiguration = GetMockConfiguration();
            var mockHttpClientFactory = GetMockHttpClientFactory();

            var mockHttpContext = A.Fake<HttpContext>();
            StringValues ignored = new StringValues("Bearer TestAccessToken");
            A.CallTo(() => mockHttpContext.Request.Headers.TryGetValue("Authorization", out ignored))
                .Returns(true);

            var mockHttpContextAccessor = A.Fake<IHttpContextAccessor>();
            A.CallTo(() => mockHttpContextAccessor.HttpContext)
                .Returns(mockHttpContext);

            var tenantId = TenantId.From("TestTenant");
            
            var authorizationHandler = new AuthenticationHandler(mockConfiguration, mockHttpClientFactory, mockHttpContextAccessor);

            var token = await authorizationHandler.GetOrCreateTokenAsync(tenantId);

            Assert.AreEqual(new StringValues("Bearer TestAccessToken"), token);
        }

        [TestMethod]
        public async Task GetOrCreateTokenAsync_NoAccessTokenPresent_UsesClientCredentialFlow()
        {
            var tenantId = TenantId.From("TestTenant");
            var issuer = "http://test.openiddict.com";
            var scopeApiPath = "http://somewhere.else.com/scope/{{tenantId}}";

            var mockConfiguration = GetMockConfiguration(issuer: issuer, scopeApiPath: scopeApiPath);
            var mockHttpClientFactory = GetMockHttpClientFactory((mockHttp) =>
            {
                mockHttp.Expect($"{issuer}/.well-known/openid-configuration")
                    .Respond("application/json", @"{ ""token_endpoint"": """ + $"{issuer}/connect/token" + @""" }");

                mockHttp.Expect($"http://somewhere.else.com/scope/{tenantId}")
                    .Respond("application/json", @"[ ""audis.analyzer.prod"" ]");

                mockHttp.Expect($"{issuer}/connect/token")
                    .Respond("application/json", @"{ ""access_token"": ""Bearer TestAccessToken"" }");
            });

            var mockHttpContext = A.Fake<HttpContext>();
            StringValues ignored = StringValues.Empty;
            A.CallTo(() => mockHttpContext.Request.Headers.TryGetValue("Authorization", out ignored))
                .Returns(false);

            var mockHttpContextAccessor = A.Fake<IHttpContextAccessor>();
            A.CallTo(() => mockHttpContextAccessor.HttpContext)
                .Returns(mockHttpContext);

            var authorizationHandler = new AuthenticationHandler(mockConfiguration, mockHttpClientFactory, mockHttpContextAccessor);

            var token = await authorizationHandler.GetOrCreateTokenAsync(tenantId);

            Assert.AreEqual(new StringValues("Bearer TestAccessToken"), token);
        }

        public static IConfiguration GetMockConfiguration(
            string issuer = default,
            string clientId = default,
            string clientSecret = default,
            string scopeApiPath = default)
        {
            issuer ??= "TestIssuer";
            clientId ??= "TestClientId";
            clientSecret ??= "TestClientSecret";
            scopeApiPath ??= "TestScopeApiPath";

            var json = @"
            {
                ""Authentication"": {
                    ""Issuer"": """ + issuer + @""",
                    ""ClientId"": """ + clientId + @""",
                    ""ClientSecret"": """ + clientSecret + @""",
                    ""ScopeApiPath"": """ + scopeApiPath + @"""
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            return configurationRoot;
        }

        public static IHttpClientFactory GetMockHttpClientFactory(Action<MockHttpMessageHandler> expectations = default)
        {
            var mockHttp = new MockHttpMessageHandler();
            expectations?.Invoke(mockHttp);

            var mockHttpClientFactory = A.Fake<IHttpClientFactory>();

            A.CallTo(() => mockHttpClientFactory.CreateClient(A<string>.Ignored))
                .ReturnsLazily(() => mockHttp.ToHttpClient());

            return mockHttpClientFactory;
        }
    }
}
