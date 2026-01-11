using System;
using System.Collections.Generic;
using Audis.Endpoints.Contract.Shared.V1;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.IntermediateDisposition.V4;

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

    public string? Logon { get; set; }

    public string? UserName { get; set; }

    required public string KnowledgeSummary { get; set; }

    required public IReadOnlyCollection<KnowledgeSummaryDto> KnowledgeSummaryItems { get; set; } =
        new List<KnowledgeSummaryDto>();

    public string? CurrentScenarioIdentifier { get; set; }

    public string? CurrentScenarioName { get; set; }

    public IEnumerable<string> CurrentScenarioDispositionCodes { get; set; } = new List<string>();

    public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();
}