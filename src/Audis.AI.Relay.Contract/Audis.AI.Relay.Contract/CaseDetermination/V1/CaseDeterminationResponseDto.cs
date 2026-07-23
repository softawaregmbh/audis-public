namespace Audis.AI.Relay.Contract.CaseDetermination.V1;

/// <summary>
///     Response body Audis expects back from the <c>Endpoints.CaseDetermination</c> relay URL
///     (outbound from the third party's perspective: what the relay must return).
/// </summary>
public record CaseDeterminationResponseDto(
    string SuggestedName,
    string SuggestedDescription,
    string Model,
    string Provider,
    long DurationMs);
