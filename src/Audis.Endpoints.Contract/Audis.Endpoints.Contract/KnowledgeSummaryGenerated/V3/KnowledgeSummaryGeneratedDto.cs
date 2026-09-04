using System;
using System.Collections.Generic;
using System.Text.Json;
using Audis.Endpoints.Contract.Shared.V1;
using Audis.Primitives;
using ScenarioDto = Audis.Endpoints.Contract.Shared.V2.ScenarioDto;
using TagDto = Audis.Endpoints.Contract.Shared.V2.TagDto;

namespace Audis.Endpoints.Contract.KnowledgeSummaryGenerated.V3;

/// <summary>
///     Outbound push notification payload for KnowledgeSummaryGenerated.
///     Audis sends this to configured endpoint URLs after a knowledge summary exists.
/// </summary>
/// <remarks>
///     Separate from the synchronous AI relay call on <c>Endpoints.InterrogationSummarized</c>,
///     which uses <c>Audis.AI.Relay.Contract.V1.ProcessRequestDto</c> /
///     <c>ProcessResponseDto</c> (Audis posts a request and expects a result back).
///     This DTO is outbound-only; no response body is required from the receiver.
/// </remarks>
public class KnowledgeSummaryGeneratedDto
{
    /// <summary>
    ///     Stable id for this endpoint event across delivery retries.
    /// </summary>
    public Guid RequestId { get; set; }

    public Guid InterrogationId { get; set; }

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

    public string? KnowledgeSummary { get; set; }

    public IReadOnlyCollection<KnowledgeSummaryDto>? KnowledgeSummaryItems { get; set; }

    public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();

    /// <summary>
    ///     Current winner per active domain (empty when none nominated yet).
    /// </summary>
    public IReadOnlyCollection<ScenarioDto> SuggestedScenarios { get; set; } = new List<ScenarioDto>();

    /// <summary>
    ///     Additional dispatch hints. Domain is optional (e.g. turntable ladder → Fw).
    /// </summary>
    public IReadOnlyCollection<TagDto> Tags { get; set; } = new List<TagDto>();

    /// <summary>
    ///     Free data object where implementation-specific data/identification/... can be stored.
    /// </summary>
    public JsonElement? Data { get; set; }
}
