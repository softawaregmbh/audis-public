using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace Audis.Authorization.Tests
{
    [TestClass]
    public class AuthorizationCoreExtensionsTests
    {
        [TestMethod]
        public void TestGetAuthorizationSettings_ClientAndResource()
        {
            var issuer = "TestIssuer";
            var clientId = "TestClientId";
            var audience = "TestAudience";
            var clientSecret = "TestClientSecret";

            var json = @"
            {
                ""Authorization"": {
                    ""Resource"": {
                        ""Issuer"": """ + issuer + @""",
                        ""Audience"": """ + audience + @""",
                        ""ClientId"": """ + clientId + @""",
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

            var authorizationSettings = configurationRoot.GetAuthorizationSettings();
            var resourceAuthorizationSettings = authorizationSettings.Resource;
            Assert.IsNotNull(authorizationSettings.Resource);
            Assert.AreEqual(issuer, resourceAuthorizationSettings.Issuer);
            Assert.AreEqual(audience, resourceAuthorizationSettings.Audience);
            Assert.AreEqual(clientId, resourceAuthorizationSettings.ClientId);

            var clientAuthorizationSettings = authorizationSettings.Client;
            Assert.IsNotNull(clientAuthorizationSettings);
            Assert.AreEqual(issuer, clientAuthorizationSettings.Issuer);
            Assert.AreEqual(clientId, clientAuthorizationSettings.ClientId);
            Assert.AreEqual(clientSecret, clientAuthorizationSettings.ClientSecret);
        }

        [TestMethod]
        public void TestGetAuthorizationSettings_ConfigurationMissing_ThrowsAuthorizationSettingsException()
        {
            var json = @"
            {
                ""Authorization"": { }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetAuthorizationSettings(), "Authorization settings are missing");
        }

        [TestMethod]
        public void TestGetAuthorizationSettings_Client_EmptyConfiguration_ThrowsAuthorizationSettingsException()
        {
            var json = @"
            {
                ""Authorization"": {
                    ""Client"": { }
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetAuthorizationSettings(), "Authorization settings are missing");
        }

        [TestMethod]
        public void TestGetAuthorizationSettings_Client()
        {
            var issuer = "TestIssuer";
            var clientId = "TestClientId";
            var clientSecret = "TestClientSecret";
            
            var json = @"
            {
                ""Authorization"": {
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

            var authorizationSettings = configurationRoot.GetAuthorizationSettings();
            var clientAuthorizationSettings = authorizationSettings.Client;
            Assert.IsNotNull(authorizationSettings.Client);
            Assert.AreEqual(issuer, clientAuthorizationSettings.Issuer);
            Assert.AreEqual(clientId, clientAuthorizationSettings.ClientId);
            Assert.AreEqual(clientSecret, clientAuthorizationSettings.ClientSecret);

            var resourceAuthorizationSettings = authorizationSettings.Resource;
            Assert.IsNull(resourceAuthorizationSettings);
        }

        [TestMethod]
        public void TestGetAuthorizationSettings_Client_IssuerMissing_ThrowsAuthorizationSettingsException()
        {
            var clientId = "TestClientId";
            var clientSecret = "TestClientSecret";

            var json = @"
            {
                ""Authorization"": {
                    ""Client"": {
                        ""ClientId"": """ + clientId + @""",
                        ""ClientSecret"": """ + clientSecret + @"""
                    }
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetAuthorizationSettings(), "Failed to setup authorization: Property 'Issuer' is not configured");
        }

        [TestMethod]
        public void TestGetAuthorizationSettings_Client_ClientIdMissing_ThrowsAuthorizationSettingsException()
        {
            var issuer = "TestIssuer";
            var clientSecret = "TestClientSecret";

            var json = @"
            {
                ""Authorization"": {
                    ""Client"": {
                        ""Issuer"": """ + issuer + @""",
                        ""ClientSecret"": """ + clientSecret + @"""
                    }
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetAuthorizationSettings(), "Failed to setup authorization: Property 'ClientId' is not configured");
        }

        [TestMethod]
        public void TestGetAuthorizationSettings_Client_ClientSecretMissing_ThrowsAuthorizationSettingsException()
        {
            var issuer = "TestIssuer";
            var clientId = "TestClientId";

            var json = @"
            {
                ""Authorization"": {
                    ""Client"": {
                        ""Issuer"": """ + issuer + @""",
                        ""ClientId"": """ + clientId + @""",
                    }
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetAuthorizationSettings(), "Failed to setup authorization: Property 'ClientSecret' is not configured");
        }

        [TestMethod]
        public void TestGetAuthorizationSettings_Resource()
        {
            var issuer = "TestIssuer";
            var clientId = "TestClientId";
            var audience = "TestAudience";

            var json = @"
            {
                ""Authorization"": {
                    ""Resource"": {
                        ""Issuer"": """ + issuer + @""",
                        ""Audience"": """ + audience + @""",
                        ""ClientId"": """ + clientId + @""",
                    }
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            var authorizationSettings = configurationRoot.GetAuthorizationSettings();
            var resourceAuthorizationSettings = authorizationSettings.Resource;
            Assert.IsNotNull(authorizationSettings.Resource);
            Assert.AreEqual(issuer, resourceAuthorizationSettings.Issuer);
            Assert.AreEqual(audience, resourceAuthorizationSettings.Audience);
            Assert.AreEqual(clientId, resourceAuthorizationSettings.ClientId);

            var clientAuthorizationSettings = authorizationSettings.Client;
            Assert.IsNull(clientAuthorizationSettings);
        }

        [TestMethod]
        public void TestGetAuthorizationSettings_Resource_EmptyConfiguration_ThrowsAuthorizationSettingsException()
        {
            var json = @"
            {
                ""Authorization"": {
                    ""Resource"": { }
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetAuthorizationSettings(), "Authorization settings are missing");
        }

        [TestMethod]
        public void TestGetAuthorizationSettings_Resource_IssuerMissing_ThrowsAuthorizationSettingsException()
        {
            var clientId = "TestClientId";
            var audience = "TestAudience";

            var json = @"
            {
                ""Authorization"": {
                    ""Client"": {
                        ""ClientId"": """ + clientId + @""",
                        ""Audience"": """ + audience + @"""
                    }
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetAuthorizationSettings(), "Failed to setup authorization: Property 'Issuer' is not configured");
        }

        [TestMethod]
        public void TestGetAuthorizationSettings_Resource_ClientIdMissing_ThrowsAuthorizationSettingsException()
        {
            var issuer = "TestIssuer";
            var audience = "TestAudience";

            var json = @"
            {
                ""Authorization"": {
                    ""Client"": {
                        ""Issuer"": """ + issuer + @""",
                        ""Audience"": """ + audience + @"""
                    }
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetAuthorizationSettings(), "Failed to setup authorization: Property 'ClientId' is not configured");
        }

        [TestMethod]
        public void TestGetAuthorizationSettings_Resource_AudienceMissing_ThrowsAuthorizationSettingsException()
        {
            var issuer = "TestIssuer";
            var clientId = "TestClientId";

            var json = @"
            {
                ""Authorization"": {
                    ""Client"": {
                        ""Issuer"": """ + issuer + @""",
                        ""ClientId"": """ + clientId + @""",
                    }
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetAuthorizationSettings(), "Failed to setup authorization: Property 'Audience' is not configured");
        }

        [TestMethod]
        public void TestGetClientAuthorizationSettings()
        {
            var issuer = "TestIssuer";
            var clientId = "TestClientId";
            var clientSecret = "TestClientSecret";

            var json = @"
            {
                ""Authorization"": {
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

            var clientAuthorizationSettings = configurationRoot.GetClientAuthorizationSettings();
            Assert.IsNotNull(clientAuthorizationSettings);
            Assert.AreEqual(issuer, clientAuthorizationSettings.Issuer);
            Assert.AreEqual(clientId, clientAuthorizationSettings.ClientId);
            Assert.AreEqual(clientSecret, clientAuthorizationSettings.ClientSecret);
        }

        [TestMethod]
        public void TestClientAuthorizationSettings_ConfigurationMissing_ThrowsAuthorizationSettingsException()
        {
            var issuer = "TestIssuer";
            var clientId = "TestClientId";
            var audience = "TestAudience";

            var json = @"
            {
                ""Authorization"": {
                    ""Resource"": {
                        ""Issuer"": """ + issuer + @""",
                        ""Audience"": """ + audience + @""",
                        ""ClientId"": """ + clientId + @""",
                    }
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetClientAuthorizationSettings(), "Client configuration is missing");
        }

        [TestMethod]
        public void TestGetResourceAuthorizationSettings()
        {
            var issuer = "TestIssuer";
            var clientId = "TestClientId";
            var audience = "TestAudience";

            var json = @"
            {
                ""Authorization"": {
                    ""Resource"": {
                        ""Issuer"": """ + issuer + @""",
                        ""Audience"": """ + audience + @""",
                        ""ClientId"": """ + clientId + @""",
                    }
                }
            }";

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json)))
                .Build();

            var resourceAuthorizationSettings = configurationRoot.GetResourceAuthorizationSettings();
            Assert.IsNotNull(resourceAuthorizationSettings);
            Assert.AreEqual(issuer, resourceAuthorizationSettings.Issuer);
            Assert.AreEqual(audience, resourceAuthorizationSettings.Audience);
            Assert.AreEqual(clientId, resourceAuthorizationSettings.ClientId);
        }

        [TestMethod]
        public void TestGetResourceAuthorizationSettings_ConfigurationMissing()
        {
            var issuer = "TestIssuer";
            var clientId = "TestClientId";
            var clientSecret = "TestClientSecret";

            var json = @"
            {
                ""Authorization"": {
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

            Assert.ThrowsException<AuthorizationSettingsException>(() => configurationRoot.GetResourceAuthorizationSettings(), "Resource configuration is missing");
        }
    }
}
