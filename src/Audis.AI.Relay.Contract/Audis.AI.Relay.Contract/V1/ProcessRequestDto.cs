using System.Text.Json;

namespace Audis.AI.Relay.Contract.V1;

/// <summary>
///     Request body for the AI Relay <c>/process</c> endpoint.
///     Also the inbound format Audis posts to <c>Endpoints.InterrogationSummarized</c>
///     when generating an AI knowledge summary (KnowledgeSummary / InterrogationSummarized path).
///     Pair with <see cref="ProcessResponseDto"/>.
/// </summary>
/// <remarks>
///     Distinct from the outbound push notification
///     <c>Audis.Endpoints.Contract.KnowledgeSummaryGenerated</c>, which Audis sends after a summary
///     exists. This type is the synchronous relay call Audis makes to obtain the AI result.
/// </remarks>
public class ProcessRequestDto
{
    required public string Input { get; init; }

    /// <summary>Supported values: "text", "json", "markdown". Default: "text".</summary>
    public string InputFormat { get; init; } = "text";

    /// <summary>Supported values: "text", "json", "bullets". Default: "text".</summary>
    public string OutputFormat { get; init; } = "text";

    public string? PromptInstruction { get; init; }

    /// <summary>Optional model override. Falls back to the relay's configured default model.</summary>
    public string? Model { get; init; }

    /// <summary>
    ///     JSON schema describing the expected output structure.
    ///     Only used when OutputFormat is "json".
    /// </summary>
    public JsonElement? OutputSchema { get; init; }

    /// <summary>
    ///     Example JSON object the LLM should mirror in structure.
    ///     Only used when OutputFormat is "json" and OutputSchema is not set.
    /// </summary>
    public JsonElement? OutputExample { get; init; }
}
