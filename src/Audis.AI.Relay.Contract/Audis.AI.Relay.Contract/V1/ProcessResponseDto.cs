namespace Audis.AI.Relay.Contract.V1;

/// <summary>
///     Response body from the AI Relay <c>/process</c> endpoint.
///     Also what Audis expects back from <c>Endpoints.InterrogationSummarized</c>
///     (KnowledgeSummary / InterrogationSummarized path).
///     Pair with <see cref="ProcessRequestDto"/>.
/// </summary>
public class ProcessResponseDto
{
    required public string Result { get; init; }

    required public string Model { get; init; }

    required public string Provider { get; init; }

    required public long DurationMs { get; init; }
}
