using System;
using System.Collections.Generic;
using Audis.Primitives;
using Newtonsoft.Json.Linq;

namespace Audis.Endpoints.Contract.InterrogationUpdated.V1
{
    public class InterrogationStepDto
    {
        public Guid InterrogationId { get; set; }
        public int ProcessStepId { get; set; }
        public DateTime Timestamp { get; set; }
        public TenantId TenantId { get; set; } = default!;
        public RevisionId RevisionId { get; set; } = default!;
        public string? ExternalId { get; set; }
        public string UserId { get; set; } = default!;
        public QuestionId? CurrentQuestionId { get; set; }
        public string? CurrentQuestion { get; set; }
        public string? CurrentQuestionType { get; set; }
        public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();
        public string? SuggestedScenarioIdentifier { get; set; }
        public string? SuggestedScenarioName { get; set; }

        /// <summary>
        /// Free data object where implementation-specific data/identification/... can be stored.
        /// </summary>
        public JObject? Data { get; set; }
    }
}
