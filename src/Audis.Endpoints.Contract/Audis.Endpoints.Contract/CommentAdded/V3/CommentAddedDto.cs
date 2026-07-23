using System;
using System.Collections.Generic;
using System.Text.Json;
using Audis.Endpoints.Contract.Shared.V1;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.CommentAdded.V3;

public class CommentAddedDto
{
    /// <summary>
    ///     Stable id for this endpoint event across delivery retries.
    /// </summary>
    public Guid RequestId { get; set; }

    public Guid CommentId { get; set; }

    public string Comment { get; set; } = default!;

    public Guid InterrogationId { get; set; }

    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     Timestamp of the current delivery attempt. Updated on retry so container logs can
    ///     correlate retry intervals for the same <see cref="RequestId"/>.
    /// </summary>
    public DateTime OriginTimestamp { get; set; }

    public TenantId TenantId { get; set; } = default!;

    public RevisionId RevisionId { get; set; } = default!;

    public string? ExternalId { get; set; }

    public string? UserId { get; set; }

    public IReadOnlyCollection<TagDto> Tags { get; set; } = new List<TagDto>();

    public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();

    /// <summary>
    ///     Free data object where implementation-specific data/identification/... can be stored.
    /// </summary>
    public JsonElement? Data { get; set; }
}
