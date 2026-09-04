using System.Collections.Generic;

namespace Audis.Endpoints.Contract.Shared.V2;

public class ScenarioDto
{
    required public string Identifier { get; set; }

    required public string Name { get; set; }

    public IReadOnlyCollection<string> DispositionCodes { get; set; } = new List<string>();

    /// <summary>
    ///     Catalog domain key (e.g. <c>fw</c>). Null when the catalog has no <c>Domains</c>
    ///     (implicit default domain).
    /// </summary>
    public string? DomainIdentifier { get; set; }

    /// <summary>
    ///     Identifier how the domain should be passed to an external API (e.g. <c>F</c> / <c>R</c>).
    /// </summary>
    public string? DomainExternalApiIdentifier { get; set; }

    public string? DomainName { get; set; }
}
