namespace Audis.AI.Relay.Contract.V1;

/// <summary>
///     Response body from the AI Relay <c>/process</c> endpoint.
///     Also what Audis expects back from <c>Endpoints.InterrogationSummarized</c>
///     (KnowledgeSummary / InterrogationSummarized path).
///     Pair with <see cref="ProcessRequestDto"/>.
/// </summary>
public record ProcessResponseDto(
    string Result,
    string Model,
    string Provider,
    long DurationMs);
