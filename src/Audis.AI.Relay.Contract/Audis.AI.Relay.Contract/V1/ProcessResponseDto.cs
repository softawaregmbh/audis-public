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

    /// <summary>Optional model metadata from the relay.</summary>
    public string? Model { get; init; }

    /// <summary>Optional provider metadata from the relay.</summary>
    public string? Provider { get; init; }

    /// <summary>Optional processing duration in milliseconds.</summary>
    public long? DurationMs { get; init; }
}
