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
        public TenantId TenantId { get; set; }
        public RevisionId RevisionId { get; set; }
        public string ExternalId { get; set; }
        public string UserId { get; set; }
        public QuestionId CurrentQuestionId { get; set; }
        public string CurrentQuestion { get; set; }
        public string CurrentQuestionType { get; set; }
        public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; }
        public ScenarioIdentifier SuggestedScenarioIdentifier { get; set; }
        public string SuggestedScenarioName { get; set; }

        /// <summary>
        /// Free data object where implementation-specific data/identification/... can be stored.
        /// </summary>
        public JObject Data { get; set; }
    }
}
