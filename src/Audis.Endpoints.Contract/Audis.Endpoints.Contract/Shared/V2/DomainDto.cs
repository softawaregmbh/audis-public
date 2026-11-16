namespace Audis.Endpoints.Contract.Shared.V2;

/// <summary>
///     Domain (BOS track) of a scenario or tag.
/// </summary>
public class DomainDto
{
    /// <summary>
    ///     Catalog domain key (e.g. <c>fw</c>).
    /// </summary>
    required public string Identifier { get; set; }

    /// <summary>
    ///     Value passed to an external API (e.g. <c>F</c> / <c>R</c>). CAD-Link value, else catalog
    ///     ExternalApiIdentifier, else short name, else identifier.
    /// </summary>
    required public string ExternalApiIdentifier { get; set; }

    public string? Name { get; set; }
}
