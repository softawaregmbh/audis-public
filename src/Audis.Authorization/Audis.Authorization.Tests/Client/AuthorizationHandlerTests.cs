using Audis.Primitives;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Audis.Authorization.Client.Tests
{
    [TestClass]
    public class AuthorizationHandlerTests
    {
        [TestMethod]
        public async Task GetOrCreateTokenAsync_AccessTokenPresent()
        {
            var mockConfiguration = GetMockConfiguration();
            var mockHttpClientFactory = GetMockHttpClientFactory();

            var mockHttpContext = A.Fake<HttpContext>();
            StringValues ignored = new StringValues("Bearer asdf");
            A.CallTo(() => mockHttpContext.Request.Headers.TryGetValue("Authorization", out ignored))
                .Returns(true);

            var tenantId = TenantId.From("TestTenant");
            
            var authorizationHandler = new AuthorizationHandler(mockConfiguration, mockHttpClientFactory, mockHttpContext);

            var token = await authorizationHandler.GetOrCreateTokenAsync(tenantId);

            Assert.AreEqual(new StringValues("Bearer asdf"), token);
        }

        [TestMethod]
        public async Task GetOrCreateTokenAsync_NoAccessTokenPresent_UsesClientCredentialFlow()
        {
            var tenantId = TenantId.From("TestTenant");
            var issuer = "http://test.issuer.com";
            var scopeApiPath = "TestScopeApiPath";

            var mockConfiguration = GetMockConfiguration(issuer: issuer, scopeApiPath: scopeApiPath);
            var mockHttpClientFactory = GetMockHttpClientFactory((mockHttp) =>
            {
                mockHttp.Expect($"{issuer}/{scopeApiPath}/{tenantId}")
                    .Respond("application/json", "audis.analyzer.prod");

                mockHttp.Expect(issuer)
                    .Respond("application/json", @"{ ""access_token"": ""Bearer asdf"" }");
            }, numberOfClients: 2);

            var mockHttpContext = A.Fake<HttpContext>();
            StringValues ignored = StringValues.Empty;
            A.CallTo(() => mockHttpContext.Request.Headers.TryGetValue("Authorization", out ignored))
                .Returns(false);


            var authorizationHandler = new AuthorizationHandler(mockConfiguration, mockHttpClientFactory, mockHttpContext);

            var token = await authorizationHandler.GetOrCreateTokenAsync(tenantId);

            Assert.AreEqual(new StringValues("Bearer asdf"), token);
        }

        public static IConfiguration GetMockConfiguration(
            string issuer = default,
            string clientId = default,
            string audience = default,
            string clientSecret = default,
            string scopeApiPath = default)
        {
            issuer ??= "TestIssuer";
            clientId ??= "TestClientId";
            audience ??= "TestAudience";
            clientSecret ??= "TestClientSecret";
            scopeApiPath ??= "TestScopeApiPath";

            var json = @"
            {
                ""Authorization"": {
                    ""Resource"": {
                        ""Issuer"": """ + issuer + @""",
                        ""Audience"": """ + audience + @""",
                        ""ClientId"": """ + clientId + @""",
                        ""ScopeApiPath"": """ + scopeApiPath + @""",
                    },
                    ""Client"": {
                        ""Issuer"": """ + issuer + @""",
                        ""ClientId"": """ + clientId + @""",
                        ""ClientSecret"": """ + clientSecret + @"""
                    }
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            return configurationRoot;
        }

        public static IHttpClientFactory GetMockHttpClientFactory(Action<MockHttpMessageHandler> expectations = default, int numberOfClients = 1)
        {
            var mockHttp = new MockHttpMessageHandler();
            expectations?.Invoke(mockHttp);

            var mockHttpClientFactory = A.Fake<IHttpClientFactory>();

            A.CallTo(() => mockHttpClientFactory.CreateClient(A<string>.Ignored))
                .ReturnsLazily(() => mockHttp.ToHttpClient())
                .NumberOfTimes(numberOfClients);

            return mockHttpClientFactory;
        }
    }
}
