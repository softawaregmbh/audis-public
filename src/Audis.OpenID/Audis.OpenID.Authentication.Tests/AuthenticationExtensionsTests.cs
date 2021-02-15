using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace Audis.OpenID.Authentication.Tests
{
    [TestClass]
    public class AuthenticationExtensionsTests
    {
        [TestMethod]
        public void TestTryGetAuthorizationCodeToken()
        {
            var httpRequest = A.Fake<HttpRequest>();
            StringValues accessToken = new StringValues("Bearer TestAccessToken");
            A.CallTo(() => httpRequest.Headers.TryGetValue("Authorization", out accessToken))
                .Returns(true);

            Assert.IsTrue(httpRequest.TryGetAuthorizationCodeToken(out StringValues result));
            Assert.AreEqual(new StringValues("Bearer TestAccessToken"), result);
        }

        [TestMethod]
        public void TestTryGetAuthorizationCodeToken_NoBearerTokenSet()
        {
            var httpRequest = A.Fake<HttpRequest>();
            StringValues accessToken = StringValues.Empty;
            A.CallTo(() => httpRequest.Headers.TryGetValue("Authorization", out accessToken))
                .Returns(false);

            Assert.IsFalse(httpRequest.TryGetAuthorizationCodeToken(out StringValues result));
            Assert.IsTrue(StringValues.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void TestTrySetAuthorizationHeader()
        {
            var httpClient = new HttpClient();
            httpClient.TrySetAuthorizationHeader("Bearer TestBearerToken");

            Assert.IsNotNull(httpClient.DefaultRequestHeaders.Authorization);
            Assert.AreEqual("Bearer", httpClient.DefaultRequestHeaders.Authorization.Scheme);
            Assert.IsNotNull("TestBearerToken", httpClient.DefaultRequestHeaders.Authorization.Parameter);
        }

        [TestMethod]
        public void TestTrySetAuthorizationHeader_AccessTokenEmpty_HeaderValueNull()
        {
            StringValues accessToken = StringValues.Empty;

            var httpClient = new HttpClient();
            httpClient.TrySetAuthorizationHeader(accessToken);

            Assert.IsNull(httpClient.DefaultRequestHeaders.Authorization);
        }
    }
}
