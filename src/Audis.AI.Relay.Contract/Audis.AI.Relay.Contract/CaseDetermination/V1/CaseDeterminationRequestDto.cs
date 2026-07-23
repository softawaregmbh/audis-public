namespace Audis.AI.Relay.Contract.CaseDetermination.V1;

/// <summary>
///     Request body Audis posts to the configured <c>Endpoints.CaseDetermination</c> relay URL
///     (inbound from the third party's perspective: what Audis expects the relay to accept).
///     Matches the AI Relay <c>/suggest/case-record</c> endpoint.
/// </summary>
public class CaseDeterminationRequestDto
{
    required public string KnowledgeJson { get; init; }

    required public string FinalScenario { get; init; }

    required public string Summary { get; init; }

    /// <summary>Optional model override. Falls back to the relay's configured default model.</summary>
    public string? Model { get; init; }
}
