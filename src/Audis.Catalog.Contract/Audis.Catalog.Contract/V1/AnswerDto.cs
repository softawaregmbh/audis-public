using Audis.Primitives;

namespace Audis.Catalog.Contract.V1;

/// <summary>
///     Answer option belonging to a catalog question.
/// </summary>
public class AnswerDto
{
    required public AnswerId Id { get; set; }

    /// <summary>
    ///     Answer text as stored in the catalog (may contain markup).
    /// </summary>
    required public string Text { get; set; }

    /// <summary>
    ///     Answer text with markup removed.
    /// </summary>
    required public string RawText { get; set; }

    /// <summary>
    ///     Answer type: <c>Selection</c> or <c>Input</c>.
    /// </summary>
    public string? Type { get; set; }

    public IReadOnlyCollection<string> Synonyms { get; set; } = Array.Empty<string>();
}
