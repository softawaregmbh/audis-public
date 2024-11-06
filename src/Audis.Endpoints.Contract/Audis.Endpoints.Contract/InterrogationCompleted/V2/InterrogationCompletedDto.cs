using System;
using System.Collections.Generic;
using Audis.Endpoints.Contract.InterrogationUpdated.V2;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.InterrogationCompleted.V2
{
    public class InterrogationCompletedDto
    {
        public Guid InterrogationId { get; set; }
        public int FinalProcessStepId { get; set; }
        public DateTime Timestamp { get; set; }
        public TenantId TenantId { get; set; } = default!;
        public RevisionId RevisionId { get; set; } = default!;
        public string? ExternalId { get; set; }
        public string? UserId { get; set; }
        public string FreeText { get; set; } = default!;
        public bool Cancelled { get; set; }
        public string? CancellationReason { get; set; }
        public string? SuggestedScenarioIdentifier { get; set; }
        public string? SuggestedScenarioName { get; set; }
        public string? SelectedScenarioIdentifier { get; set; }
        public string? SelectedScenarioName { get; set; }
        public string? SelectedScenarioReason { get; set; }
        public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();
    }
}
