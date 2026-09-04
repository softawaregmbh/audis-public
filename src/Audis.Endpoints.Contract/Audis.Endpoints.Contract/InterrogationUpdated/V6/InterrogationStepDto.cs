using System;
using System.Collections.Generic;
using System.Text.Json;
using Audis.Endpoints.Contract.Shared.V1;
using Audis.Primitives;
using ScenarioDto = Audis.Endpoints.Contract.Shared.V2.ScenarioDto;
using TagDto = Audis.Endpoints.Contract.Shared.V2.TagDto;

namespace Audis.Endpoints.Contract.InterrogationUpdated.V6;

public class InterrogationStepDto
{
    /// <summary>
    ///     Stable id for this endpoint event across delivery retries.
    /// </summary>
    public Guid RequestId { get; set; }

    public Guid InterrogationId { get; set; }

    public int ProcessStepId { get; set; }

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

    public QuestionDto? CurrentQuestion { get; set; }

    public QuestionDto? PreviousQuestion { get; set; }

    public string? KnowledgeSummary { get; set; }

    public IReadOnlyCollection<KnowledgeSummaryDto>? KnowledgeSummaryItems { get; set; }

    public IReadOnlyCollection<AnswerDto> SelectedAnswers { get; set; } = new List<AnswerDto>();

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
