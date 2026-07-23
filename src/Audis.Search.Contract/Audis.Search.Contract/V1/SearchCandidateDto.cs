using Audis.Primitives;

namespace Audis.Search.Contract.V1;

/// <summary>
///     One answer option in the searchable pool for an external search request.
///     Property names align with catalog <c>AnswerDto</c> / domain answer text and the
///     in-process search item shape (<c>Text</c>, <c>Context</c>, <c>Synonyms</c>).
/// </summary>
public class SearchCandidateDto
{
    required public AnswerId Id { get; set; }

    /// <summary>
    ///     Searchable answer text. Prefer markup-stripped text (catalog <c>RawText</c>).
    /// </summary>
    required public string Text { get; set; }

    public IReadOnlyCollection<string> Synonyms { get; set; } = Array.Empty<string>();

    /// <summary>
    ///     Optional extra search context (e.g. value of knowledge <c>#context</c>).
    /// </summary>
    public string? Context { get; set; }
}
