using System;
using System.Collections.Generic;
using System.Text.Json;
using Audis.Endpoints.Contract.Shared.V1;
using Audis.Endpoints.Contract.Shared.V2;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.InterrogationCompleted.V6;

public class InterrogationCompletedDto
{
    /// <summary>
    ///     Stable id for this endpoint event across delivery retries.
    /// </summary>
    public Guid RequestId { get; set; }

    public Guid InterrogationId { get; set; }

    public int FinalProcessStepId { get; set; }

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

    public bool Cancelled { get; set; }

    public string? CancellationReason { get; set; }

    /// <summary>
    ///     Engine suggestion per active domain.
    /// </summary>
    public IReadOnlyCollection<ScenarioDto> SuggestedScenarios { get; set; } = new List<ScenarioDto>();

    /// <summary>
    ///     Dispatcher selection per active domain (override stays inside that domain).
    ///     Reason lives on each item, not as a single interrogation-wide string.
    /// </summary>
    public IReadOnlyCollection<SelectedScenarioDto> SelectedScenarios { get; set; } =
        new List<SelectedScenarioDto>();

    /// <summary>
    ///     Additional dispatch hints. Domain is optional (e.g. turntable ladder → Fw).
    /// </summary>
    public IReadOnlyCollection<TagDto> Tags { get; set; } = new List<TagDto>();

    public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();

    /// <summary>
    ///     Free data object where implementation-specific data/identification/... can be stored.
    /// </summary>
    public JsonElement? Data { get; set; }
}
