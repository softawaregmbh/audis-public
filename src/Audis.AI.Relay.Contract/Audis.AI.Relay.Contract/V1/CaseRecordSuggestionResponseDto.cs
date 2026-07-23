namespace Audis.AI.Relay.Contract.V1;

public record CaseRecordSuggestionResponseDto(
    string SuggestedName,
    string SuggestedDescription,
    string Model,
    string Provider,
    long DurationMs);
