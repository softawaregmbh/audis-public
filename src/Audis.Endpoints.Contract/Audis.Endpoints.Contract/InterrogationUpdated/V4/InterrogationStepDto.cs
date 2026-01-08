using System;
using System.Collections.Generic;
using Audis.Endpoints.Contract.Shared.V1;
using Audis.Primitives;
using Newtonsoft.Json.Linq;

namespace Audis.Endpoints.Contract.InterrogationUpdated.V4
{
    public class InterrogationStepDto
    {
        public Guid InterrogationId { get; set; }

        public int ProcessStepId { get; set; }

        public DateTime Timestamp { get; set; }

        required public TenantId TenantId { get; set; }

        required public RevisionId RevisionId { get; set; }

        public string? ExternalId { get; set; }

        public string? Logon { get; set; }
        
        public string? UserName { get; set; }
        
        public QuestionDto? CurrentQuestion { get; set; }

        public QuestionDto? PreviousQuestion { get; set; }

        public string? KnowledgeSummary { get; set; }

        public IReadOnlyCollection<KnowledgeSummaryDto>? KnowledgeSummaryItems { get; set; }

        public IReadOnlyCollection<AnswerDto> SelectedAnswers { get; set; } = new List<AnswerDto>();

        public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();

        public ScenarioDto? SuggestedScenario { get; set; }

        /// <summary>
        /// Free data object where implementation-specific data/identification/... can be stored.
        /// </summary>
        public JObject? Data { get; set; }
    }
}
