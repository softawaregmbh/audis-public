namespace Audis.AI.Relay.Contract.V1;

public class PipelineProcessRequestDto
{
    required public string Input { get; init; }

    /// <summary>Optional model override. Falls back to the relay's configured default model.</summary>
    public string? Model { get; init; }
}
