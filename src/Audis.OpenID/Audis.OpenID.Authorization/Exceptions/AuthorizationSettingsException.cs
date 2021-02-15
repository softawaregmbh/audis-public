using System;

namespace Audis.OpenID.Authorization.Exceptions
{
    [Serializable]
    public class AuthorizationSettingsException : Exception
    {
        public AuthorizationSettingsException() { }
        public AuthorizationSettingsException(string message) : base(message) { }
        public AuthorizationSettingsException(string message, Exception inner) : base(message, inner) { }
        protected AuthorizationSettingsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
