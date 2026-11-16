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
    ///     Domain the tag is bound to. Null when the tag is not bound to a domain.
    /// </summary>
    public DomainDto? Domain { get; set; }
}
