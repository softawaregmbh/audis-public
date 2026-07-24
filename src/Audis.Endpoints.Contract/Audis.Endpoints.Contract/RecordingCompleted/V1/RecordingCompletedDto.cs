using System;
using System.Collections.Generic;
using System.Text.Json;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.RecordingCompleted.V1;

/// <summary>
///     Outbound push notification payload for RecordingCompleted / StoreRecording
///     (<c>Endpoints.RecordingCompleted</c>). Audis delivers this asynchronously;
///     no response body is expected (fire-and-forget aside from HTTP success status).
/// </summary>
public class RecordingCompletedDto
{
    /// <summary>
    ///     Stable id for this endpoint event across delivery retries.
    /// </summary>
    public Guid RequestId { get; set; }

    public Guid? Id { get; set; }

    public Guid? ClusterId { get; set; }

    public string? TraceId { get; set; }

    required public string EnvironmentName { get; set; }

    required public string OriginName { get; set; }

    required public string ContributorName { get; set; }

    public string? ContributorRef { get; set; }

    public string? DisplayName { get; set; }

    public string? Description { get; set; }

    public Guid? InterrogationId { get; set; }

    public TenantId? TenantId { get; set; }

    public RevisionId? RevisionId { get; set; }

    public int? StepCount { get; set; }

    public string? ScenarioName { get; set; }

    public string? ScenarioIdentifier { get; set; }

    required public string RecordingJson { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     Timestamp of the current delivery attempt. Updated on retry so container logs can
    ///     correlate retry intervals for the same <see cref="RequestId"/>.
    /// </summary>
    public DateTime OriginTimestamp { get; set; }

    public string? Comment { get; set; }

    /// <summary>
    ///     ExternalApiIdentifier values (fallback: Name/TagIdentifier).
    /// </summary>
    public IReadOnlyCollection<string> Tags { get; set; } = new List<string>();

    /// <summary>
    ///     Free data object where implementation-specific data/identification/... can be stored.
    /// </summary>
    public JsonElement? Data { get; set; }
}
