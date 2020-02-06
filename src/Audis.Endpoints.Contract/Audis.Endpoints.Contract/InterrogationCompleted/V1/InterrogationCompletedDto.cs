using System;
using System.Collections.Generic;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.InterrogationCompleted.V1
{
    public class InterrogationCompletedDto
    {
        public Guid InterrogationId { get; set; }
        public int FinalProcessStepId { get; set; }
        public DateTime Timestamp { get; set; }
        public TenantId TenantId { get; set; }
        public RevisionId RevisionId { get; set; }
        public string ExternalId { get; set; }
        public string UserId { get; set; }
        public string FreeText { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        public ScenarioIdentifier SuggestedScenarioIdentifier { get; set; }
        public string SuggestedScenarioName { get; set; }
        public ScenarioIdentifier SelectedScenarioIdentifier { get; set; }
        public string SelectedScenarioName { get; set; }
        public string SelectedScenarioReason { get; set; }
    }
}
