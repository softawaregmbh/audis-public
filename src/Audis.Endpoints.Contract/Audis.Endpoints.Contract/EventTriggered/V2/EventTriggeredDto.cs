using System;
using System.Collections.Generic;
using System.Text.Json;
using Audis.Endpoints.Contract.Shared.V1;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.EventTriggered.V2;

public class EventTriggeredDto
{
    public Guid EventId { get; set; }

    public string EventName { get; set; } = default!;

    public Guid InterrogationId { get; set; }

    public DateTime Timestamp { get; set; }

    public TenantId TenantId { get; set; } = default!;

    public RevisionId RevisionId { get; set; } = default!;

    public string? ExternalId { get; set; }

    public string? Logon { get; set; }

    public string? UserName { get; set; }

    public IReadOnlyCollection<KnowledgeDto> Knowledge { get; set; } = new List<KnowledgeDto>();

    public JsonElement? Data { get; set; }
}