using Audis.Primitives;

namespace Audis.Endpoints.Contract.Shared.V1;

public class TagDto
{
    required public TagIdentifier Identifier { get; set; }

    required public string Name { get; set; }

    /// <summary>
    ///     Identifier how the tag should be passed to an external API.
    ///     Falls back to <see cref="Name"/> when not specified.
    /// </summary>
    public string? ExternalApiIdentifier { get; set; }
}
