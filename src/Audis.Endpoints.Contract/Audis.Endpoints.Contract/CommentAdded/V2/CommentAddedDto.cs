using System;
using System.Collections.Generic;
using Audis.Endpoints.Contract.Shared.V1;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.CommentAdded.V2;

public class CommentAddedDto
{
    public Guid CommentId { get; set; }
    public string Comment { get; set; } = default!;
    public Guid InterrogationId { get; set; }
    public DateTime Timestamp { get; set; }
    public TenantId TenantId { get; set; } = default!;
    public RevisionId RevisionId { get; set; } = default!;
    public string? ExternalId { get; set; }
    public string? UserId { get; set; }
    public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();
}