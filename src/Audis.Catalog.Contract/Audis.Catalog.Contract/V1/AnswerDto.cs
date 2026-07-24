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

#pragma warning disable SA1010 // Collection expression; StyleCop 1.1 treats [] as indexer
    public IReadOnlyCollection<string> Synonyms { get; set; } = [];
#pragma warning restore SA1010
}
