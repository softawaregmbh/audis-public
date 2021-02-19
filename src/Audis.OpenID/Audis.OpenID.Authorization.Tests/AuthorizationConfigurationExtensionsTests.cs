using Audis.OpenID.Authorization.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace Audis.OpenID.Authorization.Configuration.Tests
{
    [TestClass]
    public class AuthorizationConfigurationExtensionsTests
    {
        [TestMethod]
        public void TestGetResourceAuthorizationSettings()
        {
            var issuer = "TestIssuer";
            var audience = "TestAudience";

            var json = @"
            {
                ""Authorization"": {
                    ""Issuer"": """ + issuer + @""",
                    ""Audience"": """ + audience + @"""
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            var authorizationSettings = configurationRoot.GetAuthorizationSettings();
            Assert.AreEqual(issuer, authorizationSettings.Issuer);
            Assert.AreEqual(audience, authorizationSettings.Audience);
        }

        [TestMethod]
        public void TestGetResourceAuthorizationSettings_EmptyConfiguration_ThrowsAuthorizationSettingsException()
        {
            var json = @"
            {
                ""Authorization"": { }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            var exception = Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetAuthorizationSettings());
            Assert.AreEqual("Failed to setup authorization: Configuration is missing", exception.Message);
        }

        [TestMethod]
        public void TestGetResourceAuthorizationSettings_IssuerMissing_ThrowsAuthorizationSettingsException()
        {
            var clientId = "TestClientId";
            var audience = "TestAudience";

            var json = @"
            {
                ""Authorization"": {
                    ""Audience"": """ + audience + @"""
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            var exception = Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetAuthorizationSettings());
            Assert.AreEqual("Failed to setup authorization: Property 'Issuer' is not configured", exception.Message);
        }

        [TestMethod]
        public void TestGetResourceAuthorizationSettings_AudienceMissing_ThrowsAuthorizationSettingsException()
        {
            var issuer = "TestIssuer";

            var json = @"
            {
                ""Authorization"": {
                    ""Issuer"": """ + issuer + @"""
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            var exception = Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetAuthorizationSettings());
            Assert.AreEqual("Failed to setup authorization: Property 'Audience' is not configured", exception.Message);
        }
    }
}
