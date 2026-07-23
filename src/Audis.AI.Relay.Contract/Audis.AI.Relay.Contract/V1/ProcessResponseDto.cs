namespace Audis.AI.Relay.Contract.V1;

public record ProcessResponseDto(
    string Result,
    string Model,
    string Provider,
    long DurationMs);
