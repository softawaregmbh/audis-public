namespace Audis.AI.Relay.Contract.V1;

/// <summary>
///     Request body for the AI Relay <c>/process/{pipeline}</c> endpoint.
///     Response shape is <see cref="ProcessResponseDto"/>.
/// </summary>
public class PipelineProcessRequestDto
{
    required public string Input { get; init; }

    /// <summary>Optional model override. Falls back to the relay's configured default model.</summary>
    public string? Model { get; init; }
}
