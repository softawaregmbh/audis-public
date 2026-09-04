using System;
using System.Collections.Generic;
using System.Text.Json;
using Audis.Endpoints.Contract.Shared.V1;
using Audis.Primitives;
using ScenarioDto = Audis.Endpoints.Contract.Shared.V2.ScenarioDto;
using TagDto = Audis.Endpoints.Contract.Shared.V2.TagDto;

namespace Audis.Endpoints.Contract.IntermediateDisposition.V6;

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

    /// <summary>
    ///     When the intermediate disposition actually fired (button press or automatic).
    ///     Distinct from <see cref="InitiatedAt"/> and <see cref="OriginTimestamp"/>.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     When this intermediate disposition first became available
    ///     (e.g. button appeared). Null when unknown or when it fired immediately.
    /// </summary>
    public DateTime? InitiatedAt { get; set; }

    /// <summary>
    ///     How it fired: <c>User</c> or <c>Automatic</c>. Null when not distinguished.
    /// </summary>
    public string? TriggerSource { get; set; }

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

    /// <summary>
    ///     Current winner per active domain at the time of the intermediate disposition.
    /// </summary>
    public IReadOnlyCollection<ScenarioDto> CurrentScenarios { get; set; } = new List<ScenarioDto>();

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
