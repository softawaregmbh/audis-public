using System;

namespace Audis.OpenID.Authorization.Exceptions
{
    [Serializable]
    public class AuthenticationSettingsException : Exception
    {
        public AuthenticationSettingsException() { }
        public AuthenticationSettingsException(string message) : base(message) { }
        public AuthenticationSettingsException(string message, Exception inner) : base(message, inner) { }
        protected AuthenticationSettingsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
