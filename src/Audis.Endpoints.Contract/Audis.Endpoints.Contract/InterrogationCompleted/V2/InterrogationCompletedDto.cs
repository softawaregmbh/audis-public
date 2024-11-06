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

        required public TenantId TenantId { get; set; }

        required public RevisionId RevisionId { get; set; }

        public string? ExternalId { get; set; }

        public string? UserId { get; set; }

        required public string FreeText { get; set; }

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
