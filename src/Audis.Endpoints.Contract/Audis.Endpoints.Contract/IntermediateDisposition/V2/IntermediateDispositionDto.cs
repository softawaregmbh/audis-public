using System;
using System.Collections.Generic;
using Audis.Endpoints.Contract.InterrogationUpdated.V2;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.IntermediateDisposition.V2
{
    public class IntermediateDispositionDto
    {
        public Guid InterrogationId { get; set; }

        public int CurrentProcessStepId { get; set; }

        required public string IntermediateDispositionName { get; set; }

        public string? IntermediateDispositionExternalIdentifier { get; set; }

        public DateTime Timestamp { get; set; }

        required public TenantId TenantId { get; set; }

        required public RevisionId RevisionId { get; set; }

        public string? ExternalId { get; set; }

        public string? UserId { get; set; }

        required public string KnowledgeSummary { get; set; }

        public string? CurrentScenarioIdentifier { get; set; }

        public string? CurrentScenarioName { get; set; }

        public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();
    }
}
