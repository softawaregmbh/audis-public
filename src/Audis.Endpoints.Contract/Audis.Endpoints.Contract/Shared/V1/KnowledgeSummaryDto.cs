using System.Text.Json;

namespace Audis.Endpoints.Contract.Shared.V1;

public class KnowledgeSummaryDto
{
    required public string Summary { get; set; }

    /// <summary>
    ///     Free data object where summary-specific metadata/types/... can be stored.
    /// </summary>
    public JsonElement? Data { get; set; }
}