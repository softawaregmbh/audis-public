using System.Collections.Generic;

namespace Audis.Endpoints.Contract.Shared.V2;

public class ScenarioDto
{
    /// <summary>
    ///     AUDIS scenario identifier.
    /// </summary>
    required public string Identifier { get; set; }

    /// <summary>
    ///     Value passed to an external API (e.g. CAD incident type): CAD-Link value (incl. combinations),
    ///     else catalog ExternalApiIdentifier, else <see cref="Identifier" />.
    /// </summary>
    required public string ExternalApiIdentifier { get; set; }

    required public string Name { get; set; }

    /// <summary>
    ///     Priority of the scenario in the external system (CAD-Link cadPriority of the scenario or
    ///     combination). Null when not mapped.
    /// </summary>
    public string? Priority { get; set; }

    /// <summary>
    ///     Domain of the scenario. All disposition codes of a scenario belong to this domain. Null for the
    ///     implicit default domain (catalog without <c>Domains</c>).
    /// </summary>
    public DomainDto? Domain { get; set; }

    /// <summary>
    ///     Disposition codes in catalog order. The first entry is the primary code (e.g. CAD keyword).
    /// </summary>
    public IReadOnlyCollection<DispositionCodeDto> DispositionCodes { get; set; } = new List<DispositionCodeDto>();
}
