using System;
using System.Collections.Generic;
using System.Text.Json;
using Audis.Endpoints.Contract.Shared.V1;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.KnowledgeSummaryGenerated.V1;

public class KnowledgeSummaryGeneratedDto
{
    public Guid InterrogationId { get; set; }
    
    public DateTime Timestamp { get; set; }

    required public TenantId TenantId { get; set; }

    required public RevisionId RevisionId { get; set; }

    public string? ExternalId { get; set; }

    public string? Logon { get; set; }

    public string? UserName { get; set; }

    public string? KnowledgeSummary { get; set; }

    public IReadOnlyCollection<KnowledgeSummaryDto>? KnowledgeSummaryItems { get; set; }
    
    public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();

    public ScenarioDto? SuggestedScenario { get; set; }

    /// <summary>
    ///     Free data object where implementation-specific data/identification/... can be stored.
    /// </summary>
    public JsonElement? Data { get; set; }
}
