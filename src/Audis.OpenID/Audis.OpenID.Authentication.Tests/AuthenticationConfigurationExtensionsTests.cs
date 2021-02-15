using Audis.OpenID.Authorization.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace Audis.OpenID.Authentication.Configuration.Tests
{
    [TestClass]
    public class AuthenticationConfigurationExtensionsTests
    {
        [TestMethod]
        public void TestGetAuthenticationSettings()
        {
            var issuer = "TestIssuer";
            var clientId = "TestClientId";
            var clientSecret = "TestClientSecret";
            var scopeApiPath = "TestScopeApiPath";

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

            var authenticationSettings = configurationRoot.GetAuthenticationSettings();
            Assert.AreEqual(issuer, authenticationSettings.Issuer);
            Assert.AreEqual(clientId, authenticationSettings.ClientId);
            Assert.AreEqual(clientSecret, authenticationSettings.ClientSecret);
            Assert.AreEqual(scopeApiPath, authenticationSettings.ScopeApiPath);
        }

        [TestMethod]
        public void TestGetAuthenticationSettings_EmptyConfiguration_ThrowsAuthorizationSettingsException()
        {
            var json = @"
            {
                ""Authentication"": { }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthenticationSettingsException>(() => configurationRoot.GetAuthenticationSettings(), "Authentication settings are missing");
        }

        [TestMethod]
        public void TestGetAuthenticationSettings_IssuerMissing_ThrowsAuthorizationSettingsException()
        {
            var clientId = "TestClientId";
            var clientSecret = "TestClientSecret";
            var scopeApiPath = "TestScopeApiPath";

            var json = @"
            {
                ""Authentication"": {
                    ""ClientId"": """ + clientId + @""",
                    ""ClientSecret"": """ + clientSecret + @""",
                    ""ScopeApiPath"": """ + scopeApiPath + @"""
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthenticationSettingsException>(() => configurationRoot.GetAuthenticationSettings(), "Failed to setup authentication: Property 'Issuer' is not configured");
        }

        [TestMethod]
        public void TestGetAuthenticationSettings_ClientIdMissing_ThrowsAuthorizationSettingsException()
        {
            var issuer = "TestIssuer";
            var clientSecret = "TestClientSecret";
            var scopeApiPath = "TestScopeApiPath";

            var json = @"
            {
                ""Authentication"": {
                    ""Issuer"": """ + issuer + @""",
                    ""ClientSecret"": """ + clientSecret + @""",
                    ""ScopeApiPath"": """ + scopeApiPath + @"""
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthenticationSettingsException>(() => configurationRoot.GetAuthenticationSettings(), "Failed to setup authentication: Property 'ClientId' is not configured");
        }

        [TestMethod]
        public void TestGetAuthenticationSettings_ClientSecretMissing_ThrowsAuthorizationSettingsException()
        {
            var issuer = "TestIssuer";
            var clientId = "TestClientId";
            var scopeApiPath = "TestScopeApiPath";

            var json = @"
            {
                ""Authentication"": {
                    ""Issuer"": """ + issuer + @""",
                    ""ClientId"": """ + clientId + @""",
                    ""ScopeApiPath"": """ + scopeApiPath + @""",
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthenticationSettingsException>(() => configurationRoot.GetAuthenticationSettings(), "Failed to setup authentication: Property 'ClientSecret' is not configured");
        }

        [TestMethod]
        public void TestGetAuthenticationSettings_ScopeApiPathMissing_ThrowsAuthorizationSettingsException()
        {
            var issuer = "TestIssuer";
            var clientId = "TestClientId";
            var clientSecret = "TestClientSecret";

            var json = @"
            {
                ""Authentication"": {
                    ""Issuer"": """ + issuer + @""",
                    ""ClientId"": """ + clientId + @""",
                    ""ClientSecret"": """ + clientSecret + @"""
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthenticationSettingsException>(() => configurationRoot.GetAuthenticationSettings(), "Failed to setup authentication: Property 'ScopeApiPath' is not configured");
        }
    }
}
