using System;
using System.Collections.Generic;
using System.Text.Json;
using Audis.Endpoints.Contract.Shared.V1;
using Audis.Endpoints.Contract.Shared.V2;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.EventTriggered.V4;

public class EventTriggeredDto
{
    /// <summary>
    ///     Stable id for this endpoint event across delivery retries.
    ///     Named <c>EventId</c> for continuity with V1–V3.
    /// </summary>
    public Guid EventId { get; set; }

    public string EventName { get; set; } = default!;

    public Guid InterrogationId { get; set; }

    /// <summary>
    ///     When the event actually fired (user action or automatic trigger).
    ///     Distinct from <see cref="InitiatedAt"/> (when it first became available)
    ///     and from <see cref="OriginTimestamp"/> (current delivery attempt).
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     When the event first became available in this interrogation
    ///     (e.g. intermediate-disposition button appeared). Null when unknown
    ///     or when the event fired in the same instant it became available.
    /// </summary>
    public DateTime? InitiatedAt { get; set; }

    /// <summary>
    ///     How the event fired: <c>User</c> (button) or <c>Automatic</c>.
    ///     Null when the producer does not distinguish.
    /// </summary>
    public string? TriggerSource { get; set; }

    /// <summary>
    ///     Timestamp of the current delivery attempt. Updated on retry so container logs can
    ///     correlate retry intervals for the same <see cref="EventId"/>.
    /// </summary>
    public DateTime OriginTimestamp { get; set; }

    public TenantId TenantId { get; set; } = default!;

    public RevisionId RevisionId { get; set; } = default!;

    public string? ExternalId { get; set; }

    public string? Logon { get; set; }

    public string? UserName { get; set; }

    /// <summary>
    ///     Additional dispatch hints. Domain is optional (e.g. turntable ladder → Fw).
    /// </summary>
    public IReadOnlyCollection<TagDto> Tags { get; set; } = new List<TagDto>();

    public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();

    /// <summary>
    ///     Current winner per active domain at the time the event fired.
    /// </summary>
    public IReadOnlyCollection<ScenarioDto> CurrentScenarios { get; set; } = new List<ScenarioDto>();

    public JsonElement? Data { get; set; }
}
