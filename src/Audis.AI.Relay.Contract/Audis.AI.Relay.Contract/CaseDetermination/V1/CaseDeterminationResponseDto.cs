namespace Audis.AI.Relay.Contract.CaseDetermination.V1;

/// <summary>
///     Response body Audis expects back from the <c>Endpoints.CaseDetermination</c> relay URL
///     (outbound from the third party's perspective: what the relay must return).
/// </summary>
public class CaseDeterminationResponseDto
{
    required public string SuggestedName { get; init; }

    required public string SuggestedDescription { get; init; }

    required public string Model { get; init; }

    required public string Provider { get; init; }

    required public long DurationMs { get; init; }
}
