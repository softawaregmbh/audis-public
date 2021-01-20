using System;
using Audis.Primitives;
using Newtonsoft.Json.Linq;

namespace Audis.Endpoints.Contract.EventTriggered.V1
{
    public class EventTriggeredDto
    {
        public Guid EventId { get; set; }

        public string EventName { get; set; } = default!;

        public Guid InterrogationId { get; set; }

        public DateTime Timestamp { get; set; }

        public TenantId TenantId { get; set; } = default!;

        public RevisionId RevisionId { get; set; } = default!;

        public string? ExternalId { get; set; }

        public string? UserId { get; set; }

        public JObject? Data { get; set; }
    }
}
