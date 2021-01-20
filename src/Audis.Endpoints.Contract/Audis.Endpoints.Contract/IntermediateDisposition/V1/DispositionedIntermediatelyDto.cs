using System;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.IntermediateDisposition.V1
{
    public class DispositionedIntermediatelyDto
    {
        public Guid InterrogationId { get; set; }

        public int CurrentProcessStepId { get; set; }

        public DateTime Timestamp { get; set; }

        public TenantId TenantId { get; set; } = default!;

        public RevisionId RevisionId { get; set; } = default!;

        public string? ExternalId { get; set; }

        public string? UserId { get; set; }

        public string KnowledgeSummary { get; set; } = default!;

        public string? CurrentScenarioIdentifier { get; set; }
        
        public string? CurrentScenarioName { get; set; }
    }
}
