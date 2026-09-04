namespace Audis.Endpoints.Contract.Shared.V2;

/// <summary>
///     Additional dispatch hint (e.g. turntable ladder because the location is on the 7th floor).
///     Domain is optional: bound tags go with that BOS track, unbound tags apply to the whole interrogation.
/// </summary>
public class TagDto
{
    required public string Identifier { get; set; }

    /// <summary>
    ///     Value passed to an external API (catalog ExternalApiIdentifier, else name / identifier).
    /// </summary>
    required public string ExternalApiIdentifier { get; set; }

    /// <summary>
    ///     Catalog domain key (e.g. <c>fw</c>). Null when the tag is not bound to a domain.
    /// </summary>
    public string? DomainIdentifier { get; set; }

    /// <summary>
    ///     Identifier how the domain should be passed to an external API (e.g. <c>F</c> / <c>R</c>).
    /// </summary>
    public string? DomainExternalApiIdentifier { get; set; }

    public string? DomainName { get; set; }
}
