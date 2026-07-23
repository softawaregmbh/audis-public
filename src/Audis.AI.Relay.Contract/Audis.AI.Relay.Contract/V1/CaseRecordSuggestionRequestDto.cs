namespace Audis.AI.Relay.Contract.V1;

public class CaseRecordSuggestionRequestDto
{
    required public string KnowledgeJson { get; init; }

    required public string FinalScenario { get; init; }

    required public string Summary { get; init; }

    /// <summary>Optional model override. Falls back to the relay's configured default model.</summary>
    public string? Model { get; init; }
}
