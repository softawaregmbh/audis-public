using System;
using System.Collections.Generic;
using System.Text.Json;
using Audis.Endpoints.Contract.Shared.V1;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.EventTriggered.V3;

public class EventTriggeredDto
{
    /// <summary>
    ///     Stable id for this endpoint event across delivery retries.
    ///     Named <c>EventId</c> for continuity with V1/V2.
    /// </summary>
    public Guid EventId { get; set; }

    public string EventName { get; set; } = default!;

    public Guid InterrogationId { get; set; }

    public DateTime Timestamp { get; set; }

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
    ///     ExternalApiIdentifier values (fallback: Name/TagIdentifier).
    /// </summary>
    public IReadOnlyCollection<string> Tags { get; set; } = new List<string>();

    public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();

    public JsonElement? Data { get; set; }
}
