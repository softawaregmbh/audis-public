namespace Audis.Endpoints.Contract.Shared.V2;

/// <summary>
///     Disposition code of a scenario.
/// </summary>
public class DispositionCodeDto
{
    /// <summary>
    ///     AUDIS disposition code (e.g. <c>RD4</c>).
    /// </summary>
    required public string Code { get; set; }

    /// <summary>
    ///     Value passed to an external API (e.g. <c>RD-4</c>): CAD-Link value, else <see cref="Code" />.
    /// </summary>
    required public string ExternalApiIdentifier { get; set; }

    /// <summary>
    ///     Priority of this disposition code: CAD-Link cadPriority when set, else the catalog disposition
    ///     level priority. Null when unknown.
    /// </summary>
    public string? Priority { get; set; }
}
