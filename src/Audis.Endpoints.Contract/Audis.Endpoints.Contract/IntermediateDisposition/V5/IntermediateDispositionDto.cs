using System;
using System.Collections.Generic;
using System.Text.Json;
using Audis.Endpoints.Contract.Shared.V1;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.IntermediateDisposition.V5;

public class IntermediateDispositionDto
{
    /// <summary>
    ///     Stable id for this endpoint event across delivery retries.
    /// </summary>
    public Guid RequestId { get; set; }

    public Guid InterrogationId { get; set; }

    public int CurrentProcessStepId { get; set; }

    required public string IntermediateDispositionName { get; set; }

    public string? IntermediateDispositionExternalIdentifier { get; set; }

    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     Timestamp of the current delivery attempt. Updated on retry so container logs can
    ///     correlate retry intervals for the same <see cref="RequestId"/>.
    /// </summary>
    public DateTime OriginTimestamp { get; set; }

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

    /// <summary>
    ///     ExternalApiIdentifier values (fallback: Name/TagIdentifier).
    /// </summary>
    public IReadOnlyCollection<string> Tags { get; set; } = new List<string>();

    public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();

    /// <summary>
    ///     Free data object where implementation-specific data/identification/... can be stored.
    /// </summary>
    public JsonElement? Data { get; set; }
}
