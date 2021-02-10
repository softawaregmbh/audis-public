using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audis.Authorization
{
    public class ResourceAuthorizationSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string ClientId { get; set; }

        public string ScopeApiPath { get; set; }

        public void Deconstruct(out string issuer, out string audience)
        {
            issuer = this.Issuer;
            audience = this.Audience;
        }

        public void Deconstruct(out string issuer, out string audience, out string clientId)
        {
            issuer = this.Issuer;
            audience = this.Audience;
            clientId = this.ClientId;
        }
    }
}
